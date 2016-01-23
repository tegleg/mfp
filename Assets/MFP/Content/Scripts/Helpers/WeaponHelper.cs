using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DestroyIt
{
    public static class WeaponHelper
    {
        /// <summary>Launches a weapon from the player's controller transform.</summary>
        /// <param name="weaponPrefab">The weapon to launch in front of the player.</param>
        /// <param name="player">The transform of the player controller.</param>
        /// <param name="startDistance">The initial distance from the player transform to instantiate the weapon.</param>
        /// <param name="initialVelocity">The initial force velocity applied to the weapon (if any). For example, a bullet fired from a gun.</param>
        /// <param name="randomRotation">If TRUE, the weapon prefab will be instantiated and rotated randomly before launch.</param>
        public static void Launch(GameObject weaponPrefab, Transform weaponLauncher, float startDistance, float initialVelocity, bool forceCollisions, bool randomRotation)
        {
            Quaternion rotation = randomRotation ? UnityEngine.Random.rotation : weaponLauncher.rotation;

            // Instantiate the projectile.
            var startPos = weaponLauncher.TransformPoint(Vector3.forward * startDistance);
            var projectile = ObjectPool.Instance.Spawn(weaponPrefab, startPos, rotation);
            var projectileRbody = projectile.GetComponent<Rigidbody>();

            //if (forceCollisions && projectile.GetComponent<ForceCollisions>() == null)
            //    projectile.AddComponent<ForceCollisions>();

            // Get the fire direction based on where the player is facing and apply force to propel it forward.
            if (projectileRbody != null)
            {
                projectileRbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                projectileRbody.velocity = Vector3.zero; // zero out the velocity
                if (initialVelocity > 0.0f)
                    projectileRbody.AddForce(weaponLauncher.forward * initialVelocity, ForceMode.Impulse);
            }
        }

        /// <summary>Gets the next weapon type available, or cycles back to the beginning if there are no more.</summary>
        public static WeaponType GetNext(WeaponType currentWeaponType)
        {
            return Enum.GetValues(typeof(WeaponType)).Cast<WeaponType>().SkipWhile(e => e != currentWeaponType).Skip(1).FirstOrDefault();
        }
    }
}