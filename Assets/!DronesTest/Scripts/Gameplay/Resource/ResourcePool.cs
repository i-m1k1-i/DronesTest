using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace DronesTest.Gameplay
{
    public class ResourcePool : IPoolHandler<ResourceBase>
    {
        const int DefaultCapacity = 10;
        const int MaxSize = 100;

        private readonly IResourceProvider _resourceProvider;
        private readonly ResourceBase _prefab;
        private readonly ObjectPool<ResourceBase> _pool;
        private readonly DiContainer _container;

        public ResourcePool(ResourceBase prefab, IResourceProvider resourceProvider, DiContainer container)
        {
            _pool =  new ObjectPool<ResourceBase>(
                createFunc: OnCreate,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: obj => GameObject.Destroy(obj.gameObject),
                collectionCheck: true,
                defaultCapacity: DefaultCapacity,
                maxSize: MaxSize
                    );

            _resourceProvider = resourceProvider;
            _prefab = prefab;
            _container = container;
        }

        public ResourceBase Get()
        {
            return _pool.Get();
        }

        public void Release(ResourceBase resource)
        {
            _pool.Release(resource);
        }

        private ResourceBase OnCreate()
        {
            var resource = _container.InstantiatePrefabForComponent<ResourceBase>(
                _prefab,
                Vector3.zero,
                Quaternion.identity,
                null
                );

            return resource;
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
