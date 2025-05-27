using UnityEngine;
using Zenject;

namespace DronesTest.Gameplay
{
    public abstract class ResourceBase : MonoBehaviour, IPoolable
    {
        private IPoolReleaser<ResourceBase> _pool;

        public bool IsCollected { get; private set; }
        public bool IsAvailable { get; private set; } = true; // Indicates if the resource already is target of a drone

        [Inject]
        private void Construct(IPoolReleaser<ResourceBase> pool)
        {
            _pool = pool;
        }

        private void OnEnable()
        {
            IsCollected = false;
            IsAvailable = true;
        }

        public void ReturnToPool()
        {
            Debug.Log("Pool: " + _pool);
            _pool?.Release(this);
        }

        public virtual void OnCollect()
        {
            IsCollected = true;
        }

        public void Tag()
        {
            IsAvailable = false;
        }
    }
}