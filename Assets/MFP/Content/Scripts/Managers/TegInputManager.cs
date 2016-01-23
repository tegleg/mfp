using System.Collections;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace DestroyIt
{
    /// <summary>This script manages all player input.</summary>
    public class TegInputManager : MonoBehaviour
    {
        public Camera MyCam;
        public GameObject cannonballPrefab;         // The cannonball prefab to launch.
        public float cannonballVelocity = 75f;      // Launch velocity of the cannonball.
        public GameObject rocketPrefab;			    // The rocket prefab to launch.
        public GameObject bulletPrefab;
        public GameObject strikeDirtPrefab;
        public GameObject strikeConcretePrefab;
        public GameObject strikeWoodPrefab;
        public GameObject strikeGlassPrefab;
        public GameObject strikeMetalPrefab;
        public GameObject strikeRubberPrefab;
        public GameObject strikeStuffingPrefab;
        public ParticleSystem muzzleFlash;
        public ParticleSystem cannonFire;
        public ParticleSystem rocketFire;
        public Light muzzleLight;
        public int meleeDamage = 30;                // The amount of damage a melee attack does to its target.
        public int bulletDamage = 15;               // the amount of damage the bullet does to its target
        public float bulletForcePerSecond = 25f;    // the amount of force the bullet applies to impacted rigidbodies
        public float bulletForceFrequency = 10f;
        [Range(1, 30)]
        public int gunShotsPerSecond = 8;           // The gun's shots per second (rate of fire) while Fire1 button is depressed.
        public float startDistance = 1.5f; 		    // The distance projectiles/missiles will start in front of the player.
        public WeaponType startingWeapon = WeaponType.Rocket;   // The weapon the player will start with.
        public GameObject nukePrefab;
        public float shockwaveSpeed = 800f;         // How fast the shockwave expands (ie, how much force is applied to the shock walls).
        public GameObject shockWallPrefab;
        public GameObject dustWallPrefab;
        public int dustWallDistance = 120;          // How far in front of the shockwave should the dust effect around a player trigger?
        public GameObject groundChurnPrefab;
        public int nukeDistance = 2500;             // The distance the nuke starts away from the player.
        public int groundChurnDistance = 90;
        [Range(0.1f, .5f)]
        public float timeSlowSpeed = 0.25f;
        public Camera drawCallCamera;
        public Light sun;
        public GameObject windZone;
        public Destructible[] explosionTestObjects;
        public WeaponType SelectedWeapon { get; set; }

        private bool timeSlowed;
        private bool timeStopped;
        private float timeBetweenShots;
        private float meleeAttackDelay;
        private float lastShotTime;
        private float lastMeleeTime;
        private HeadsUpDisplay hud;
        private int playerPrefShowReticle = -1;
        private int playerPrefShowHud = -1;
        private float nukeTimer;
        public Transform firstPersonController;
        private Transform gunTransform;             // The transform where the gun will be fired from.
        private Transform cannonTransform;          // The transform where the cannon will be fired from.
        private Transform rocketTransform;          // The transform where the rocket will be fired from.
        private Transform nukeTransform;            // The location of the nuke firing controller.
        private Transform axeTransform;             // The location of the melee weapon.
        private Transform meleeDamageArea;          // The position at which melee damage will occur.

        // Hide the default constructor (use InputManager.Instance instead).
        private TegInputManager() { }

        public static TegInputManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (MyCam != null)
                hud = MyCam.GetComponent<HeadsUpDisplay>();
            else
            {
                Debug.LogWarning("No main camera found.");
                return;
            }

            firstPersonController = transform.parent;// Camera.main.transform.parent;

            foreach (Transform trans in MyCam.transform)
            {
                switch (trans.name)
                {
                    case "WeaponPosition-Nuke":
                        nukeTransform = trans;
                        break;
                    case "WeaponPosition-Gun":
                        gunTransform = trans;
                        break;
                    case "WeaponPosition-Axe":
                        axeTransform = trans;
                        break;
                    case "WeaponPosition-Cannon":
                        cannonTransform = trans;
                        break;
                    case "WeaponPosition-Rocket":
                        rocketTransform = trans;
                        break;
                    case "Melee Damage Area":
                        meleeDamageArea = trans;
                        break;
                }
            }

            if (muzzleLight != null && muzzleLight.enabled)
                muzzleLight.enabled = false;

            timeBetweenShots = 1f / gunShotsPerSecond;
            meleeAttackDelay = 0.6f; // Limit melee attacks to one every 1/2 second.
            lastShotTime = 0f;
            lastMeleeTime = 0f;

            SetTimeScale();

            // Set active weapon from player preferences.
            int playerPrefWeapon = PlayerPrefs.GetInt("SelectedWeapon", -1);
            if (playerPrefWeapon == -1)
                SelectedWeapon = startingWeapon;
            else
                SelectedWeapon = (WeaponType)playerPrefWeapon;
            SetActiveWeapon();

            // Set reticle visibility on HUD from player preferences.
            playerPrefShowReticle = PlayerPrefs.GetInt("ShowReticle", -1);
            playerPrefShowHud = PlayerPrefs.GetInt("ShowHud", -1);
            SetReticleVisibility();
            SetHudVisibility();
        }

        private void Update()
        {
            if (nukeTimer > 0f)
                nukeTimer -= Time.deltaTime;
            if (nukeTimer < 0f) nukeTimer = 0f;

            if (Cursor.visible)
            {
                return;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if (Cursor.lockState != CursorLockMode.Locked)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;

                }

                switch (SelectedWeapon)
                {
                    case WeaponType.Cannonball:
                        if (cannonFire != null)
                        {
                            cannonFire.GetComponent<ParticleSystem>().Clear(true);
                            cannonFire.Play(true);
                        }
                        WeaponHelper.Launch(cannonballPrefab, cannonTransform, startDistance, cannonballVelocity, true, true);
                        break;
                    case WeaponType.Rocket:
                        if (rocketFire != null)
                        {
                            rocketFire.GetComponent<ParticleSystem>().Clear(true);
                            rocketFire.Play(true);
                        }
                        WeaponHelper.Launch(rocketPrefab, rocketTransform, startDistance + .1f, 6f, true, false);
                        break;
                    case WeaponType.Nuke: // Nuclear Blast and Rolling Shockwave Damage
                        if (nukeTimer <= 0f)
                        {
                            FadeIn flashEffect = gameObject.AddComponent<FadeIn>();
                            flashEffect.startColor = Color.white;
                            flashEffect.fadeLength = 5f;

                            // position the nuke 2500m in front of where the player is facing.
                            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
                            Vector3 nukeForwardPos = player.position + player.forward * nukeDistance;
                            Vector3 nukePos = new Vector3(nukeForwardPos.x, 0f, nukeForwardPos.z);
                            if (groundChurnPrefab != null)
                            {
                                GameObject groundChurn = Instantiate(groundChurnPrefab, nukePos, Quaternion.identity) as GameObject;
                                Follow followScript = groundChurn.AddComponent<Follow>();
                                followScript.isPositionFixed = true;
                                followScript.objectToFollow = player;
                                followScript.facingDirection = FacingDirection.FixedPosition;
                                followScript.fixedFromPosition = nukePos;
                                followScript.fixedDistance = groundChurnDistance;
                            }
                            GameObject nuke = Instantiate(nukePrefab, nukePos, Quaternion.Euler(Vector3.zero)) as GameObject;
                            nuke.transform.LookAt(player);

                            // Configure Wind Zone
                            if (windZone != null)
                            {
                                windZone.transform.position = nukeForwardPos;
                                windZone.transform.LookAt(player);
                                Invoke("EnableWindZone", 5f);
                                DisableAfter disableAfter = windZone.GetComponent<DisableAfter>();
                                if (disableAfter == null)
                                    disableAfter = windZone.AddComponent<DisableAfter>();
                                disableAfter.seconds = 25f;
                                disableAfter.removeScript = true;
                            }

                            // Configure Dust Wall
                            if (dustWallPrefab != null)
                            {
                                GameObject dustWall = Instantiate(dustWallPrefab, nukeForwardPos, Quaternion.Euler(Vector3.zero)) as GameObject;
                                dustWall.transform.LookAt(player);
                                dustWall.transform.position += (dustWall.transform.forward * dustWallDistance);
                                dustWall.GetComponent<Rigidbody>().AddForce(dustWall.transform.forward * 800f, ForceMode.Force);
                                DustWall dwScript = dustWall.GetComponent<DustWall>();
                                dwScript.fixedFromPosition = nukePos;
                            }

                            // Configure Shock Wall
                            if (shockWallPrefab != null)
                            {
                                GameObject shockWall = Instantiate(shockWallPrefab, nukeForwardPos, Quaternion.Euler(Vector3.zero)) as GameObject;
                                shockWall.transform.LookAt(player);
                                shockWall.GetComponent<Rigidbody>().AddForce(shockWall.transform.forward * 800f, ForceMode.Force);
                                ShockWall swScript = shockWall.GetComponent<ShockWall>();
                                swScript.origin = nukePos;
                            }

                            // If the main camera has a ToneMapping component, configure it it.
                            Tonemapping toneMapping = MyCam.GetComponent<Tonemapping>();
                            if (toneMapping != null && toneMapping.enabled)
                            {
                                toneMapping.type = Tonemapping.TonemapperType.AdaptiveReinhard;
                                toneMapping.middleGrey = .5f;
                                toneMapping.white = 2.4f;
                                toneMapping.adaptionSpeed = 2f;
                                toneMapping.adaptiveTextureSize = Tonemapping.AdaptiveTexSize.Square256;
                            }

                            // Darken the sky
                            gameObject.AddComponent<BlendSkyBox>();
                            Invoke("ResetSkyBox", 25f);

                            nukeTimer = 30f; // only one nuke every XX seconds.
                        }
                        break;
                    case WeaponType.Gun:
                        FireGun();
                        break;
                    case WeaponType.Melee:
                        if (Time.time >= (lastMeleeTime + meleeAttackDelay))
                            MeleeAttack();
                        break;
                }
            }

            // Continuous fire from holding the button down
            if (Input.GetButton("Fire1") && SelectedWeapon == WeaponType.Gun && Time.time >= (lastShotTime + timeBetweenShots))
                FireGun();

            // Continuous melee attack from holding the button down (useful for chopping trees in an MMO/survival game)
            if (Input.GetButton("Fire1") && SelectedWeapon == WeaponType.Melee && Time.time >= (lastMeleeTime + meleeAttackDelay))
                MeleeAttack();

            // Time Slow
            if (Input.GetKeyUp("t"))
            {
                timeSlowed = !timeSlowed;
                SetTimeScale();
            }

            // Time Stop
            if (Input.GetKeyUp("y"))
            {
                timeStopped = !timeStopped;
                SetTimeScale();
            }

            // Draw-Call scenario camera
            Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            if (Input.GetKeyUp("c") && mainCamera != null && drawCallCamera != null && sun != null)
            {
                if (mainCamera.enabled)
                {
                    mainCamera.enabled = false;
                    drawCallCamera.enabled = true;
                    sun.shadows = LightShadows.None;
                }
                else
                {
                    mainCamera.enabled = true;
                    drawCallCamera.enabled = false;
                    sun.shadows = LightShadows.Soft;
                }
            }

            // Explosive Damage Test
            if (Input.GetKeyUp("9"))
            {
                if (explosionTestObjects != null && explosionTestObjects.Length > 0)
                {
                    foreach (Destructible destObj in explosionTestObjects)
                        destObj.ApplyImpactDamage(new ImpactInfo { Damage = 100 });
                }
                else
                    Debug.Log("DestroyIt: No explosion test objects found. Set them up in the InputManager script, attached to _GameManager.");
            }

            // Do this every frame for rigidbodies that enter the scene, so they have smooth frame interpolation.
            // TODO: can probably run this more efficiently at a set rate, like a few times per second - not every frame.
            if (timeSlowed)
            {
                foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
                {
                    foreach (Rigidbody rb in go.GetComponentsInChildren<Rigidbody>())
                        rb.interpolation = RigidbodyInterpolation.Interpolate;
                }
            }

            if (Input.GetKey("r"))
                Application.LoadLevel(Application.loadedLevel);
            if (Input.GetKeyUp("q"))
            {
                SelectedWeapon = WeaponHelper.GetNext(SelectedWeapon);
                PlayerPrefs.SetInt("SelectedWeapon", (int)SelectedWeapon);
                SetActiveWeapon();
            }
            if (Input.GetKeyUp("o"))
            {
                if (playerPrefShowReticle == -1)
                    playerPrefShowReticle = 0;
                else
                    playerPrefShowReticle = -1;

                PlayerPrefs.SetInt("ShowReticle", playerPrefShowReticle);
                SetReticleVisibility();
            }
            if (Input.GetKeyUp("h"))
            {
                if (playerPrefShowHud == -1)
                    playerPrefShowHud = 0;
                else
                    playerPrefShowHud = -1;

                PlayerPrefs.SetInt("ShowHud", playerPrefShowHud);
                SetHudVisibility();
            }
        }

        private void EnableWindZone()
        {
            if (windZone != null)
                windZone.SetActive(true);
        }

        private void SetReticleVisibility()
        {
            if (hud != null)
            {
                if (playerPrefShowReticle == -1)
                    hud.showReticle = true;
                else
                    hud.showReticle = false;
            }
        }

        private void SetHudVisibility()
        {
            if (hud != null)
            {
                if (playerPrefShowHud == -1)
                    hud.showHud = true;
                else
                    hud.showHud = false;
            }
        }

        private void SetActiveWeapon()
        {
            switch (SelectedWeapon)
            {
                case WeaponType.Cannonball:
                    gunTransform.gameObject.SetActive(false);
                    cannonTransform.gameObject.SetActive(true);
                    rocketTransform.gameObject.SetActive(false);
                    nukeTransform.gameObject.SetActive(false);
                    axeTransform.gameObject.SetActive(false);
                    break;
                case WeaponType.Gun:
                    gunTransform.gameObject.SetActive(true);
                    cannonTransform.gameObject.SetActive(false);
                    rocketTransform.gameObject.SetActive(false);
                    nukeTransform.gameObject.SetActive(false);
                    axeTransform.gameObject.SetActive(false);
                    break;
                case WeaponType.Rocket:
                    rocketTransform.gameObject.SetActive(true);
                    cannonTransform.gameObject.SetActive(false);
                    gunTransform.gameObject.SetActive(false);
                    nukeTransform.gameObject.SetActive(false);
                    axeTransform.gameObject.SetActive(false);
                    break;
                case WeaponType.Nuke:
                    rocketTransform.gameObject.SetActive(false);
                    gunTransform.gameObject.SetActive(false);
                    cannonTransform.gameObject.SetActive(false);
                    axeTransform.gameObject.SetActive(false);
                    nukeTransform.gameObject.SetActive(true);
                    break;
                case WeaponType.Melee:
                    rocketTransform.gameObject.SetActive(false);
                    gunTransform.gameObject.SetActive(false);
                    cannonTransform.gameObject.SetActive(false);
                    nukeTransform.gameObject.SetActive(false);
                    axeTransform.gameObject.SetActive(true);
                    break;
            }
        }

        private void MeleeAttack()
        {
            Animation anim = axeTransform.GetComponentInChildren<Animation>();
            anim.Play("Axe Swinging");
            lastMeleeTime = Time.time;
            Invoke("DoMeleeDamage", .2f);
        }

        private void DoMeleeDamage()
        {
            Collider[] objectsInRange = Physics.OverlapSphere(meleeDamageArea.position, .75f);

            // Apply force and damage to colliders and rigidbodies in range of the explosion
            foreach (Collider col in objectsInRange)
            {
                // Ignore terrain colliders
                if (col is TerrainCollider)
                    continue;

                // Check if weapon struck wood, concrete, glass, etc. through use of the TagIt system (Note: You can swap this out with your own multi-tagging system)
                TagIt tagIt = col.GetComponent<TagIt>();
                if (tagIt != null && tagIt.tags.Count > 0)
                {
                    GameObject prefab;
                    if (tagIt.tags.Contains(Tag.Wood))
                        prefab = Instance.strikeWoodPrefab;
                    else if (tagIt.tags.Contains(Tag.Glass))
                        prefab = Instance.strikeGlassPrefab;
                    else if (tagIt.tags.Contains(Tag.Metal))
                        prefab = Instance.strikeMetalPrefab;
                    else if (tagIt.tags.Contains(Tag.Rubber))
                        prefab = Instance.strikeRubberPrefab;
                    else if (tagIt.tags.Contains(Tag.Stuffing))
                        prefab = Instance.strikeStuffingPrefab;
                    else // Default is concrete effect
                        prefab = Instance.strikeConcretePrefab;

                    ObjectPool.Instance.Spawn(prefab, meleeDamageArea.position, Quaternion.LookRotation(meleeDamageArea.forward * -1));
                }
                else // Default is concrete effect
                    ObjectPool.Instance.Spawn(Instance.strikeConcretePrefab, meleeDamageArea.position, Quaternion.LookRotation(meleeDamageArea.forward * -1));

                // Apply impact force to rigidbody hit
                Rigidbody rbody = col.attachedRigidbody;
                if (rbody != null)
                    rbody.AddForceAtPosition(firstPersonController.forward * 3f, firstPersonController.position, ForceMode.Impulse);

                // Apply damage if object hit was Destructible
                Destructible destObj = col.gameObject.GetComponentInParent<Destructible>();
                if (destObj != null)
                {
                    ImpactInfo meleeImpact = new ImpactInfo() { Damage = meleeDamage, AdditionalForce = 150f, AdditionalForcePosition = firstPersonController.position, AdditionalForceRadius = 2f };
                    destObj.ApplyImpactDamage(meleeImpact);
                }
            }
        }

        private void FireGun()
        {
            // Play muzzle flash particle effect
            if (muzzleFlash != null)
                muzzleFlash.Emit(1);

            // Turn on muzzle flash point light
            if (muzzleLight != null && !muzzleLight.enabled)
            {
                muzzleLight.enabled = true;
                Invoke("DisableMuzzleLight", 0.1f);
            }

            // Check to see if we should launch a bullet. If a raycast hits something 10 meters in front 
            // of the player, don't instantiate a bullet. Just play the hit effect at the hit location.
            Ray ray = new Ray(gunTransform.position, gunTransform.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 10f))
                ProcessBulletHit(hitInfo, gunTransform.forward);
            else
                WeaponHelper.Launch(bulletPrefab, gunTransform, 0f, 0f, false, false); // Launch bullet

            lastShotTime = Time.time;
        }

        private void DisableMuzzleLight()
        {
            if (muzzleLight != null && muzzleLight.enabled)
                muzzleLight.enabled = false;
        }

        private void SetTimeScale()
        {
            if (timeStopped)
            {
                Time.timeScale = 0f;
                return;
            }

            if (timeSlowed)
            {
                Time.timeScale = timeSlowSpeed;
                foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
                {
                    foreach (Rigidbody rb in go.GetComponentsInChildren<Rigidbody>())
                        rb.interpolation = RigidbodyInterpolation.Interpolate;
                }
            }
            else
            {
                Time.timeScale = 1.0f;
                foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
                {
                    foreach (Rigidbody rb in go.GetComponentsInChildren<Rigidbody>())
                        rb.interpolation = RigidbodyInterpolation.None;
                }
            }
        }

        private void ResetSkyBox()
        {
            BlendSkyBox resetSkyBox = gameObject.AddComponent<BlendSkyBox>();
            resetSkyBox.reverseBlend = true;

            // If the main camera has a ToneMapping component, deactivate it.
            Tonemapping toneMapping = MyCam.GetComponent<Tonemapping>();
            if (toneMapping != null && toneMapping.enabled)
                toneMapping.type = Tonemapping.TonemapperType.UserCurve;
        }

        public void ProcessBulletHit(RaycastHit hitInfo, Vector3 bulletDirection)
        {
            // Check if bullet struck dirt
            if (hitInfo.transform.GetComponent<Terrain>() != null)
            {
                ObjectPool.Instance.Spawn(Instance.strikeDirtPrefab, hitInfo.point, Quaternion.LookRotation(Vector3.up));
                return;
            }

            // Check if bullet struck wood, concrete, glass, etc. through use of the TagIt system (Note: You can swap this out with your own multi-tagging system)
            TagIt tagIt = hitInfo.collider.GetComponent<TagIt>();
            if (tagIt != null && tagIt.tags.Count > 0)
            {
                GameObject prefab;
                if (tagIt.tags.Contains(Tag.Wood))
                    prefab = Instance.strikeWoodPrefab;
                else if (tagIt.tags.Contains(Tag.Glass))
                    prefab = Instance.strikeGlassPrefab;
                else if (tagIt.tags.Contains(Tag.Metal))
                    prefab = Instance.strikeMetalPrefab;
                else if (tagIt.tags.Contains(Tag.Rubber))
                    prefab = Instance.strikeRubberPrefab;
                else if (tagIt.tags.Contains(Tag.Stuffing))
                    prefab = Instance.strikeStuffingPrefab;
                else // Default is concrete effect
                    prefab = Instance.strikeConcretePrefab;

                ObjectPool.Instance.Spawn(prefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            }
            else // Default is concrete effect
                ObjectPool.Instance.Spawn(Instance.strikeConcretePrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

            // Apply damage if object hit was Destructible
            Destructible destObj = hitInfo.collider.gameObject.GetComponentInParent<Destructible>();
            if (destObj != null)
            {
                ImpactInfo bulletImpact = new ImpactInfo() { Damage = Instance.bulletDamage, AdditionalForce = Instance.bulletForcePerSecond, AdditionalForcePosition = hitInfo.point, AdditionalForceRadius = .5f };
                destObj.ApplyImpactDamage(bulletImpact);
            }

            Vector3 force = bulletDirection * (Instance.bulletForcePerSecond / Instance.bulletForceFrequency);

            // Apply impact force to rigidbody hit
            Rigidbody rbody = hitInfo.collider.attachedRigidbody;
            if (rbody != null)
                rbody.AddForceAtPosition(force, hitInfo.point, ForceMode.Impulse);

            // Check for Chip-Away Debris
            ChipAwayDebris chipAwayDebris = hitInfo.collider.gameObject.GetComponent<ChipAwayDebris>();
            if (chipAwayDebris != null)
                chipAwayDebris.BreakOff(force, hitInfo.point);
        }
    }
}