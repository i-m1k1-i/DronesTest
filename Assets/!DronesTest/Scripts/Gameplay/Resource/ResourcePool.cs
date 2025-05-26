using UnityEngine;
using UnityEngine.Pool;

namespace DronesTest.Gameplay
{
    public class ResourcePool : IPoolHandler<ResourceBase>
    {
        const int DefaultCapacity = 10;
        const int MaxSize = 100;

        private readonly IResourceProvider _resourceProvider;
        private readonly ObjectPool<ResourceBase> _pool;

        public ResourcePool(ResourceBase prefab, IResourceProvider resourceProvider)
        {
            _pool =  new ObjectPool<ResourceBase>(
                createFunc: () => GameObject.Instantiate(prefab),
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: obj => GameObject.Destroy(obj.gameObject),
                collectionCheck: true,
                defaultCapacity: DefaultCapacity,
                maxSize: MaxSize
                    );

            _resourceProvider = resourceProvider;
        }

        public ResourceBase Get()
        {
            return _pool.Get();
        }

        public void Release(ResourceBase resource)
        {
            _pool.Release(resource);
        }

        private void OnGet(ResourceBase obj)
        {
            _resourceProvider.Register(obj);
            obj.gameObject.SetActive(true);
        }

        private void OnRelease(ResourceBase obj)
        {
            _resourceProvider.Unregister(obj);
            obj.gameObject.SetActive(false);
        }
    }
}
