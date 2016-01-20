using UnityEngine;

namespace DestroyIt
{
    /// <summary>
    /// This script prevents fast-moving objects from passing through thin colliders.
    /// NOTE: When using this script, you should go into your Project Settings -> Physics and un-check Raycasts Hit Triggers. 
    ///       This will prevent the script from trying to force collisions with trigger colliders.
    /// </summary>
    public class ForceCollisions : MonoBehaviour
    {
        public float leadingModifier = 0.05f;   // How far ahead the object will raycast for collisions. Use the smallest number possible that still gets good results.
                                                // Too small, and you may miss collisions. Too large, and you may see the object skip ahead before colliding.
        private Rigidbody rbody;
        private float minBoundsExtent, minExtentSquared;
        private Vector3 prevPos;
        private float maxColliderSize;

        /// <summary>The position the object was at when it pre-impacted the Destructible object. Used for post-destruction resetting of impact object's position.</summary>
        public Vector3 ImpactPosition { get; set; }
        bool preSoftenDestructible;
        private ImpactInfo impactInfo;
        Destructible destructibleObj;

        private void Awake()
        {
            ImpactPosition = Vector3.zero;
            rbody = this.GetComponent<Rigidbody>();
            prevPos = rbody.transform.position;
            Collider coll = rbody.GetComponentInChildren<Collider>();
            Vector3 extents = coll.bounds.extents;
            Vector3 colliderBounds = coll.bounds.size;
            maxColliderSize = Mathf.Max(colliderBounds.x, colliderBounds.y, colliderBounds.z);
            minBoundsExtent = Mathf.Min(new float[] { extents.x, extents.y, extents.z });
            minExtentSquared = (minBoundsExtent * minBoundsExtent);
        }
        
        private void Update()
        {
            if (preSoftenDestructible && destructibleObj != null && impactInfo != null)
            {
                destructibleObj.ApplyImpactDamage(impactInfo);
                impactInfo = null;
                destructibleObj = null;
                preSoftenDestructible = false;
            }
        }
        
        private void FixedUpdate()
        {
            Vector3 movement = rbody.transform.position - prevPos; // get the amount of movement based on the previous position
            float movementMag = Mathf.Sqrt(movement.sqrMagnitude);
            preSoftenDestructible = false;

            if (movement.sqrMagnitude > minExtentSquared) // if movement is significant (beyond extents)
            {
                Vector3 lineStartPos = rbody.transform.position + (rbody.velocity.normalized * maxColliderSize);
                Vector3 lineEndPos = lineStartPos + (rbody.velocity.normalized * (movementMag + leadingModifier));
                RaycastHit hit;

                // Debug.DrawLine(rbody.position, lineEndPos, Color.red); // show leading raycast

                if (Physics.Linecast(rbody.transform.position, lineEndPos, out hit))
                {
                    rbody.transform.position = hit.point - (movement / movementMag) * leadingModifier;
                    ImpactPosition = rbody.transform.position;
                    Destroy(this);
                }
            }
            prevPos = rbody.transform.position; // reset the previous position every physics update
        }
    }
}