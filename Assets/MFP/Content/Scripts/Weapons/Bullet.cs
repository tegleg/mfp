using UnityEngine;
using System.Collections;

namespace DestroyIt
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 300f;          // meters per second
        public float timeToLive = 0.5f;     // total number of seconds the bullet will live, regardless of distance traveled


        private float spawnTime = 0f;
        private float rayCastLength;
        private bool hitSomething = false;
        private bool isInitialized = false;

        public void OnEnable()
        {
            spawnTime = Time.time;
            rayCastLength = (speed * Time.deltaTime) + 10f; // the extra 10f is to allow some overlap. otherwise, you get bullets passing through things without registering hits.
            hitSomething = false;
        }

        public void Start()
        {
            isInitialized = true;
        }

        public void Update()
        {
            if (!isInitialized) return;

            // Move the bullet
            transform.position += transform.forward * speed * Time.deltaTime;

            // Raycast in front of the bullet to see if it has hit anything.
            //Debug.DrawRay(transform.position, transform.forward, Color.red);
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayCastLength))
            {
                //Debug.Log("hit something");
                InputManager.Instance.ProcessBulletHit(hitInfo, transform.forward);
                hitSomething = true;
            }

            // Check if the bullet needs to be destroyed.
            if (Time.time > spawnTime + timeToLive || hitSomething)
                ObjectPool.Instance.PoolObject(this.gameObject);
        }
    }
}