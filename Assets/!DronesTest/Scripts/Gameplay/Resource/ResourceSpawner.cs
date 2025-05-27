using UnityEngine;
using Zenject;

namespace DronesTest.Gameplay
{
    public class ResourceSpawner : MonoBehaviour
    {
        [SerializeField, Range(1, 10)] private float _spawnInterval = 2f;
        [SerializeField] private SpawnZone _spawnZone;

        private IPoolGetter<ResourceBase> _pool;
        private float _lastSpawnTime;

        [Inject]
        private void Construct(IPoolGetter<ResourceBase> pool)
        {
            _pool = pool;
        }

        private void Update()
        {
            if (Time.timeSinceLevelLoad - _lastSpawnTime >= _spawnInterval)
            {
                Spawn();
                _lastSpawnTime = Time.timeSinceLevelLoad;
            }
        }

        private void Spawn()
        {
            ResourceBase resource = _pool.Get();
            Vector3 spawnPosition = _spawnZone.GetRandomPosition();

            resource.transform.position = spawnPosition;
        }

        [System.Serializable]
        public struct SpawnZone
        {
            public Vector3 Minimum;
            public Vector3 Maximum;

            public readonly Vector3 GetRandomPosition()
            {
                float x = Random.Range(Minimum.x, Maximum.x);
                float y = Random.Range(Minimum.y, Maximum.y);
                float z = Random.Range(Minimum.z, Maximum.z);
                return new Vector3(x, y, z);
            }
        }
    }
}