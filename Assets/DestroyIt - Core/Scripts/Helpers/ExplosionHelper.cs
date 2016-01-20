﻿using UnityEngine;

namespace DestroyIt
{
    public static class ExplosionHelper
    {
        //TODO: IsExposedToBlast method is not used right now. Need to determine if it's worth fixing, and if so, how to fix it. 
        //TODO: It was not reporting objects being exposed to blasts when at steep angles to, or in contact with, the object.
        /// <summary>Determines whether a collider object was exposed to a blast, by raycasting.</summary>
        /// <param name="gameObj">The game object that may be exposed to the blast.</param>
        /// <param name="explosion">The details about the blast.</param>
        public static bool IsExposedToBlast(this GameObject gameObj, ExplosionInfo explosion)
        {
            RaycastHit hit;
            if (Physics.Raycast(explosion.Position, (gameObj.transform.position - explosion.Position), out hit, explosion.Radius))
            {
                Collider[] colliders = gameObj.GetComponentsInChildren<Collider>();
                foreach (Collider coll in colliders)
                {
                    if (hit.collider == coll)
                    {
                        //Debug.DrawRay(explosion.Position, gameObj.transform.position - explosion.Position, Color.red, 5f);
                        return true;
                    }
                }
                //Debug.DrawRay(explosion.Position, gameObj.transform.position - explosion.Position, Color.gray, 5f);
            }
            return false;
        }

        /// <summary>Adjusts an explosion's blast effect by the provided amount.</summary>
        /// <param name="effectAmount">Amount to adjust blast effect by, between 0 (no blast damage) and 2 (double normal damage).</param>
        public static ExplosionInfo AdjustEffect(this ExplosionInfo explosion, float effectAmount)
        {
            effectAmount = Mathf.Clamp(effectAmount, 0f, 2f);
            return new ExplosionInfo()
            {
                BlastForce = explosion.BlastForce * effectAmount,
                Position = explosion.Position,
                Radius = explosion.Radius,
                UpwardModifier = explosion.UpwardModifier
            };
        }

        /// <summary>Applies impact/explosive force to debris and collider for a realistic effect.</summary>
        /// <remarks>Force/Impact is reduced by the destroyed object's VelocityReduction amount (from XML config file).</remarks>
        public static void ApplyForcesToDebris<T>(GameObject destroyedObj, float velocityReduction, T damageInfo)
        {
            if (destroyedObj == null) { return; }

            Rigidbody[] debrisRigidbodies = destroyedObj.GetComponentsInChildren<Rigidbody>();

            // For Explosive Damage: Apply force to exposed debris from direction explosion originated, modified by proximity to the blast.
            if (damageInfo.GetType() == typeof(ExplosionInfo))
            {
                ExplosionInfo explosion = damageInfo as ExplosionInfo;

                // Apply explosive force to each debris piece. (NOTE: decided not to check if each piece of debris was exposed to the blast, because that effect was inaccurate when raycasting from steep angles.)
                foreach (Rigidbody debrisRigidbody in debrisRigidbodies)
                    debrisRigidbody.AddExplosionForce(explosion.BlastForce, explosion.Position, explosion.Radius, explosion.UpwardModifier);
            }

            if (damageInfo.GetType() == typeof(ImpactInfo))
            {
                ImpactInfo impact = damageInfo as ImpactInfo;
                if (impact != null && impact.AdditionalForce > 0f)
                {
                    foreach (Rigidbody debrisRigidbody in debrisRigidbodies)
                        debrisRigidbody.AddExplosionForce(impact.AdditionalForce, impact.AdditionalForcePosition, impact.AdditionalForceRadius, 0f);
                }
            }
        }
    }
}