using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DestroyIt
{
    public static class DestructionExtensions
    {
        public static void Update(this List<float> models, int withinSeconds)
        {
            if (models.Count > 0)
            {
                for (int i = 0; i < models.Count; i++)
                {
                    if (Time.time > (models[i] + withinSeconds))
                        models.Remove(models[i]);
                }
            }
        }

        public static void ReleaseClingingDebris(this Destructible destroyedObj, GameObject parentUnder)
        {
            List<Transform> clingingDebris = new List<Transform>();
            foreach (Transform trans in destroyedObj.transform)
            {
                if (trans.gameObject.HasTag(Tag.ClingingDebris))
                    clingingDebris.Add(trans);
            }
            foreach (Transform trans in clingingDebris)
            {
                trans.parent = parentUnder.transform;
                trans.gameObject.AddComponent<Rigidbody>();
            }
        }

        public static void MakeDebrisCling(this GameObject destroyedObj)
        {
            // Check to see if any debris pieces will be clinging to nearby rigidbodies
            ClingPoint[] clingPoints = destroyedObj.GetComponentsInChildren<ClingPoint>();
            for (int i=0; i<clingPoints.Length; i++)
            {
                Rigidbody clingPointRbody = clingPoints[i].transform.parent.GetComponent<Rigidbody>();
                if (clingPointRbody == null) continue;

                // Check percent chance first
                if (clingPoints[i].chanceToCling < 100) // 100% chance always clings
                {
                    int randomNbr = Random.Range(1, 100);
                    if (randomNbr > clingPoints[i].chanceToCling) // exit if random number is outside the possible chance.
                        continue;
                }

                // Check if there's anything to cling to.
                Ray ray = new Ray(clingPoints[i].transform.position - (clingPoints[i].transform.forward * 0.025f), clingPoints[i].transform.forward); // need to start the ray behind the transform a little
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, 0.075f))
                {
                    if (hitInfo.collider.isTrigger) continue; // ignore trigger colliders.

                    clingPointRbody.transform.parent = hitInfo.collider.gameObject.transform;

                    // If the debris is Destructible, add DestructibleParent script to the parent so debris will get OnCollisionEnter() events.
                    if (clingPointRbody.gameObject.GetComponent<Destructible>() && !hitInfo.collider.gameObject.GetComponent<DestructibleParent>())
                        hitInfo.collider.gameObject.AddComponent<DestructibleParent>();

                    // If the object this debris is clinging to is also destructible, set it up so it will be released with the parent object is destroyed.
                    Destructible destructibleObj = hitInfo.collider.gameObject.GetComponentInParent<Destructible>();
                    if (destructibleObj != null)
                    {
                        destructibleObj.unparentOnDestroy.Add(clingPointRbody.gameObject);
                        DelayedRigidbody delayedRbody = clingPointRbody.gameObject.AddComponent<DelayedRigidbody>();
                        delayedRbody.mass = clingPointRbody.mass;
                        delayedRbody.drag = clingPointRbody.drag;
                        delayedRbody.angularDrag = clingPointRbody.angularDrag;
                    }
                    // Remove all cling points from this clinging debris object
                    ClingPoint[] clingPointsToDestroy = clingPointRbody.gameObject.GetComponentsInChildren<ClingPoint>();
                    for (int j = 0; j < clingPointsToDestroy.Length; j++)
                        Object.Destroy(clingPointsToDestroy[j].gameObject);
                        
                    // Remove all rigidbodies from this clinging debris object
                    clingPointRbody.gameObject.RemoveAllFromChildren<Rigidbody>();
                }
            }
        }

        public static void ProcessDestructibleCollision(this Destructible destructibleObj, Collision collision, Rigidbody collidingRigidbody)
        {
            // Ignore collisions if collidingRigidbody is null
            if (collidingRigidbody == null) return;

            // Ignore collisions if this object is destroyed.
            if (destructibleObj.IsDestroyed) return;

            // Check that the impact is forceful enough to cause damage
            if (collision.relativeVelocity.magnitude < 2f) return;

            if (collision.contacts.Length == 0) return;

            float impactDamage;
            Rigidbody otherRbody = collision.contacts[0].otherCollider.attachedRigidbody;

            // If we've collided with another rigidbody, use the average mass of the two objects for impact damage.
            if (otherRbody != null)
            {
                float avgMass = (otherRbody.mass + collidingRigidbody.mass) / 2;
                impactDamage = Vector3.Dot(collision.contacts[0].normal, collision.relativeVelocity) * avgMass;
            }
            else // If we've collided with a static object (terrain, static collider, etc), use this object's attached rigidbody.
                impactDamage = Vector3.Dot(collision.contacts[0].normal, collision.relativeVelocity) * collidingRigidbody.mass;

            impactDamage = Mathf.Abs(impactDamage); // can't have negative damage

            if (impactDamage > 1f) // impact must do at least 1 damage to bother with.
            {
                //Debug.Log("Impact Damage: " + impactDamage);
                //Debug.DrawRay(otherRbody.transform.position, collision.relativeVelocity, Color.yellow, 10f); // yellow: where the impact force is heading
                ImpactInfo impactInfo = new ImpactInfo() { ImpactObject = otherRbody, Damage = (int)impactDamage, ImpactObjectVelocityFrom = collision.relativeVelocity * -1 };
                destructibleObj.ApplyImpactDamage(impactInfo);
            }
        }

        /// <summary>When a Destructible Object is DAMAGED, this script will attempt to find and reassign the appropriate damaged materials.</summary>
        public static void ApplyDamageToMaterials(this Destructible obj, int damageLevel)
        {
            if (damageLevel < 1) damageLevel = 1;
            if (damageLevel > 4) damageLevel = 4;

            // Get Destructible children first, so we can rule out their children that have DamageMaterials.
            List<GameObject> disabledGameObjects = new List<GameObject>();
            Destructible[] destructibles = obj.GetComponentsInChildren<Destructible>();
            for (int i = 0; i < destructibles.Length; i++)
            {
                if (destructibles[i] != obj)
                {
                    destructibles[i].gameObject.SetActive(false);
                    disabledGameObjects.Add(destructibles[i].gameObject);
                }
            }

            // Now get all the DamageMaterials that do not have Destructible parents that are children of the destructible we are applying damage to. (confused yet?)
            DamageMaterials[] dMats = obj.GetComponentsInChildren<DamageMaterials>();

            // Let's re-enable the disabled Destructible children now.
            foreach (GameObject disabledObj in disabledGameObjects)
                disabledObj.SetActive(true);

            if (dMats == null)
            {
                UnityEngine.Debug.Log("DamageMaterials script not found on destructible object \"" + obj.name + "\"");
                return;
            }

            for (int i = 0; i < dMats.Length; i++)
            {
                // ignore clinging debris - we don't want to reset the damage stage of debris clinging to this object.
                if (dMats[i].gameObject.HasTag(Tag.ClingingDebris)) continue;

                //if (dMats[i].gameObject)
                dMats[i].SetDamageLevel(damageLevel);
            }
        }
    }
}