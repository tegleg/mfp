using UnityEngine;
using System.Collections;

namespace DestroyIt
{
    public class CharacterControllerCollision : MonoBehaviour
    {
        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.moveLength >= 0.15) // If this was a hard enough hit... (adjust to what fits your game)
            {
                // See if the object hit is a destructible object
                Destructible destructibleObj = hit.collider.gameObject.GetComponentInParent<Destructible>();
                if (destructibleObj != null)
                {
                    // Does the destructible object have a rigidbody? If so, we could get the impact velocity and whatnot...
                    Rigidbody otherRbody = hit.collider.attachedRigidbody;
                    if (otherRbody != null)
                        Debug.Log("Hit rigidbody!");
                    else
                        Debug.Log("Hit non-rigidbody!");

                    // ...but for our simple scenario, just apply 50 points of damage with a hard hit.
                    destructibleObj.ApplyDamage(50);
                }
            }
        }
    }
}

