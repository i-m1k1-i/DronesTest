using UnityEngine;
using Zenject;

namespace DronesTest.Gameplay
{
    public abstract class ResourceBase : MonoBehaviour, IPoolable
    {
        private IPoolReleaser<ResourceBase> _pool;

        public bool IsCollected { get; private set; }

        [Inject]
        private void Construct(IPoolReleaser<ResourceBase> pool)
        {
            _pool = pool;
        }

        private void OnEnable()
        {
            IsCollected = false;
        }

        public void ReturnToPool()
        {
            _pool?.Release(this);
        }

        public void OnCollect()
        {
            IsCollected = true;
        }
    }
}