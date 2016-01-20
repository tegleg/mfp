using UnityEngine;
using System.Collections;

//TODO: Is this script really needed for our asset?

namespace DestroyIt
{
    public class JointCleanup : MonoBehaviour
    {
        //TODO: Get rid of nextUpdate strategy and use InvokeRepeating() structure.
        private const float updateFrequency = .5f; // The time (in seconds) this script updates.
        private float nextUpdate;

        // Use this for initialization
        private void Start()
        {
            nextUpdate = Time.time + updateFrequency;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Time.time > nextUpdate)
            {
                Joint[] joints = gameObject.GetComponentsInChildren<Joint>();
                if (joints.Length > 0)
                {
                    for (int i = 0; i < joints.Length; i++)
                    {
                        if (joints[i].connectedBody == null)
                            Destroy(joints[i]);
                    }
                }
                else // script is no longer needed if there are no joints to cleanup.
                    Destroy(this);

                nextUpdate = Time.time + updateFrequency;
            }
        }
    }
}