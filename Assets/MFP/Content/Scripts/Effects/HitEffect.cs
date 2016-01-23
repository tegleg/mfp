using UnityEngine;

namespace DestroyIt
{
    /// <summary>
    /// Attach this to any rigidbody object that acts as a projectile and may collide with 
    /// Destructible objects. This script will play particle effects when the object hits 
    /// something, and will do damage to Destructible objects.
    /// </summary>
    public class HitEffect : MonoBehaviour
    {
        public float velocityThreshold = 10f; // impact velocity must be at least this amount to play a hit effect.

        public void OnCollisionEnter(Collision collision)
        {
            // Check that the impact is forceful enough to cause damage
            if (collision.relativeVelocity.magnitude < velocityThreshold) return;

            if (collision.contacts.Length == 0) return;

            // Check if object struck dirt
            if (collision.contacts[0].otherCollider.GetComponent<Terrain>() != null)
            {
                ObjectPool.Instance.Spawn(InputManager.Instance.strikeDirtPrefab, collision.contacts[0].point, Quaternion.LookRotation(Vector3.up));
                return;
            }

            // Check if bullet struck wood, concrete, glass, etc. through use of the TagIt system (Note: You can swap this out with your own multi-tagging system)
            TagIt tagIt = collision.contacts[0].otherCollider.GetComponent<TagIt>();
            GameObject effect = null;
            if (tagIt != null && tagIt.tags.Count > 0)
            {
                if (tagIt.tags.Contains(Tag.Wood))
                    effect = InputManager.Instance.strikeWoodPrefab;
                else if (tagIt.tags.Contains(Tag.Glass))
                    effect = InputManager.Instance.strikeGlassPrefab;
                else if (tagIt.tags.Contains(Tag.Metal))
                    effect = InputManager.Instance.strikeMetalPrefab;
                else // Default is concrete effect
                    effect = InputManager.Instance.strikeConcretePrefab;
            }
            else // Default is concrete effect
                effect = InputManager.Instance.strikeConcretePrefab;

            if (effect != null)
                ObjectPool.Instance.Spawn(effect, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));

            // Apply impact damage to Destructible objects without rigidbodies
            Destructible destructibleObj = collision.contacts[0].otherCollider.gameObject.GetComponentInParent<Destructible>();
            if (destructibleObj != null && collision.contacts[0].otherCollider.attachedRigidbody == null)
                destructibleObj.ProcessDestructibleCollision(collision, this.GetComponent<Rigidbody>());

            // Check for Chip-Away Debris
            ChipAwayDebris chipAwayDebris = collision.contacts[0].otherCollider.gameObject.GetComponent<ChipAwayDebris>();
            if (chipAwayDebris != null) 
                chipAwayDebris.BreakOff(collision.relativeVelocity * -1, collision.contacts[0].point);
        }
    }
}