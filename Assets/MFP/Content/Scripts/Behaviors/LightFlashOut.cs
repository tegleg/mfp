using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Effect: This script makes the lights of its parent glow bright before popping.
///
/// This script is designed to be put on a ParticleSystem object that is placed as a child 
/// under an object that has lights. For example, a Sconce gameobject has a light under it.
/// A particle effect BulbFlash is added as a child under the Sconce. This script would be
/// placed on the BulbFlash.
/// </summary>
namespace DestroyIt
{
    public class LightFlashOut : MonoBehaviour
    {
        public float flashOutTime = 0.16f; // The total time for the bulb to get bright and then pop (die out).

        private bool anyLightsOn;
        private List<Light> lightsToFlashOut;
        private float flashOutTimer;
        private Transform parent;

        void OnEnable()
        {
            flashOutTimer = flashOutTime;
            lightsToFlashOut = new List<Light>();
            parent = this.gameObject.transform.parent;
            if (parent == null)
                parent = this.transform;

            // Add each light under the parent object to the list of bulbs to "flash out".
            foreach (Light lit in parent.gameObject.GetComponentsInChildren<Light>())
            {
                if (lit != null && lit.enabled)
                {
                    anyLightsOn = true;
                    lightsToFlashOut.Add(lit);
                }
            }
        }

        void Update()
        {
            if (!anyLightsOn) return;

            if (flashOutTimer <= 0)
            {
                foreach (Light lit in lightsToFlashOut)
                {
                    // Turn off light
                    lit.enabled = false;
                    // Remove any PoweredLight scripts.
                    if (lit.gameObject.GetComponent<PoweredLight>() != null)
                        lit.gameObject.RemoveComponent<PoweredLight>();
                }
            }
            else
            {
                foreach (Light lit in lightsToFlashOut)
                    lit.intensity += 0.03f;

                flashOutTimer -= Time.deltaTime;
            }
        }
    }
}