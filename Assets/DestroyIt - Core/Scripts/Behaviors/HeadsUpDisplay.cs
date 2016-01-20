using System;
using System.Text;
using UnityEngine;

namespace DestroyIt
{
    /// <summary>This script provides a head's up display with some basic controls for the demo scene.</summary>
    public class HeadsUpDisplay : MonoBehaviour
    {
        public bool showHud = true;
        public bool showHighDetailCounter = true;   // set to TRUE to show how many high detail models have been created within X seconds.
        public bool showDebrisQueue = true;         // set to TRUE to show how many debris pieces are currently in the world.
        public bool showParticleCounter = true;     // set to TRUE to show how many particles are currently in the game.
        public bool showKeyCommands = true;
        public bool showFramesPerSecond = true;
        public bool showSelectedWeapon = true;
        public bool showReticle = true;
        public Texture reticleTexture;
        public Color reticleColor = new Color(1, 1, 1, .5f);
        public Texture timeSlowTexture;

        private int frameCount;
        private float nextUpdate;
        private float fps;
        private float updateRate = 5.0f; // updates per second
        private Texture2D rectTexture;
        private GUIStyle rectStyle;

        void Start()
        {
            if (showReticle && reticleTexture == null)
                Debug.Log("[HeadsUpDisplay]: No reticle texture assigned. Reticle will not be displayed.");
        }

        void Update()
        {
            frameCount++;
            if (Time.time > nextUpdate)
            {
                nextUpdate = Time.time + (1.0f / updateRate);
                fps = frameCount * updateRate;
                frameCount = 0;
            }
        }

        void OnGUI()
        {
            if (showReticle && reticleTexture != null)
            {
                GUI.color = reticleColor;
                GUI.DrawTexture(new Rect(Screen.width / 2 - (reticleTexture.width * 0.5f), Screen.height / 2 - (reticleTexture.height * 0.5f), reticleTexture.width, reticleTexture.height), reticleTexture);
                GUI.color = Color.white;
            }

            if (!showHud) return;

            StringBuilder label = new StringBuilder();
            
            if (showKeyCommands || showFramesPerSecond || showHighDetailCounter || showDebrisQueue || showParticleCounter ||
                DestructionManager.Instance.MonitoredObjects.Count > 0 || showSelectedWeapon || showReticle)
            {
                GUIDrawRect(new Rect(0f, 0f, 260f, 90f + DestructionManager.Instance.MonitoredObjects.Count * 20f), new Color32(30,30,30,150));
            }

            if (showKeyCommands)
                label.Append("[R] Reset Level\t\t\t[Q] Switch Weapon\n[T] Time Slow\t\t\t[Y] Time Stop\n[O] Toggle Reticle\t\t[H] Toggle HUD\n\n");
            
            if (showFramesPerSecond)
                label.AppendFormat("FPS: {0}", fps);

            if (showHighDetailCounter)
            {
                if (DestructionManager.Instance.IsDestroyedPrefabLimitReached)
                    label.AppendFormat("\n<color={2}>Destroyed Prefabs (last {0}s): {1}</color>", DestructionManager.Instance.withinSeconds, DestructionManager.Instance.DestroyedPrefabCounter.Count, "red");
                else if (DestructionManager.Instance.DestroyedPrefabCounter.Count > DestructionManager.Instance.destroyedPrefabLimit / 2)
                    label.AppendFormat("\n<color={2}>Destroyed Prefabs (last {0}s): {1}</color>", DestructionManager.Instance.withinSeconds, DestructionManager.Instance.DestroyedPrefabCounter.Count, "yellow");
                else
                    label.AppendFormat("\n<color={2}>Destroyed Prefabs (last {0}s): {1}</color>", DestructionManager.Instance.withinSeconds, DestructionManager.Instance.DestroyedPrefabCounter.Count, "white");
            }
            if (showParticleCounter && ParticleManager.Instance != null)
            {
                if (ParticleManager.Instance.IsMaxActiveParticles)
                    label.AppendFormat("\n<color={2}>Destroyed Particles (last {0}s): {1}</color>", ParticleManager.Instance.withinSeconds, ParticleManager.Instance.ActiveParticles.Length, "red");
                else if (ParticleManager.Instance.ActiveParticles.Length > ParticleManager.Instance.maxDestroyedParticles / 2)
                    label.AppendFormat("\n<color={2}>Destroyed Particles (last {0}s): {1}</color>", ParticleManager.Instance.withinSeconds, ParticleManager.Instance.ActiveParticles.Length, "yellow");
                else
                    label.AppendFormat("\n<color={2}>Destroyed Particles (last {0}s): {1}</color>", ParticleManager.Instance.withinSeconds, ParticleManager.Instance.ActiveParticles.Length, "white");
            }
            if (showDebrisQueue)
            {
                if (DestructionManager.Instance.ActiveDebrisCount > DestructionManager.Instance.maxPersistentDebris)
                    label.AppendFormat("\n<color={1}>Debris count: {0}</color>", DestructionManager.Instance.ActiveDebrisCount, "yellow");
                else
                    label.AppendFormat("\n<color={1}>Debris count: {0}</color>", DestructionManager.Instance.ActiveDebrisCount, "white");
            }
  
            if (DestructionManager.Instance.MonitoredObjects.Count > 0)
            {
                label.Append("\n-------------------------------------------------------");
                foreach (Destructible obj in DestructionManager.Instance.MonitoredObjects)
                {
                    if (obj != null)
                        label.AppendFormat("\n{0} {1}: {2} hp", obj.name, obj.transform.position, obj.currentHitPoints);
                }
            }

            GUI.Label(new Rect(20, 10, 300, 220), label.ToString());

            if (timeSlowTexture != null)
            {
                GUI.color = Color.white;
                GUI.DrawTexture(new Rect(Screen.width *.75f, 0f, timeSlowTexture.width, timeSlowTexture.height), timeSlowTexture);
            }
        }

        public void GUIDrawRect(Rect position, Color color)
        {
            if (rectTexture == null)
                rectTexture = new Texture2D(1, 1);

            if (rectStyle == null)
                rectStyle = new GUIStyle();

            rectTexture.SetPixel(0, 0, color);
            rectTexture.Apply();
            rectStyle.normal.background = rectTexture;
            
            GUI.Box(position, GUIContent.none, rectStyle);
        }
    }
}