﻿using UnityEngine;

namespace DestroyIt
{
    public class PoolAfter : MonoBehaviour
    {
        public float seconds;           // seconds to wait before re-pooling this game object.
        public bool reenableChildren;   // determines whether to re-enable all child objects when this object is pooled.
        public bool removeWhenPooled;   // Remove this script when the object is pooled?
        public bool resetToPrefab;      // Reset the entire object back to prefab? (This means it will destroy and recreate the object.)

        private float timeLeft;
        private bool isInitialized;

        void Start()
        {
            timeLeft = seconds;
            isInitialized = true;
        }

        void OnEnable()
        {
            timeLeft = seconds;
        }

        void Update()
        {
            if (!isInitialized) return;

            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                if (resetToPrefab)
                {
                    GameObject objectToPool = ObjectPool.Instance.SpawnFromOriginal(this.gameObject.name);
                    if (objectToPool != null)
                        ObjectPool.Instance.PoolObject(objectToPool);
                    
                    Destroy(this.gameObject);
                    isInitialized = false;
                    return;
                }

                if (removeWhenPooled)
                    Destroy(this);

                ObjectPool.Instance.PoolObject(this.gameObject, reenableChildren);
            }
        }
    }
}