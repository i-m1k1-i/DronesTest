using System.Collections.Generic;
using UnityEngine;

namespace DronesTest.Gameplay
{
    public class ResourceManager : IResourceFinder, IResourceProvider
    {
        private readonly HashSet<ResourceBase> _resources = new();

        public ResourceManager()
        {
            var resources = GameObject.FindObjectsByType<ResourceBase>(FindObjectsSortMode.None);
            foreach (var resource in resources)
            {
                Register(resource);
            }
        }

        public void Register(ResourceBase resource)
        {
            _resources.Add(resource);
        }

        public void Unregister(ResourceBase resource)
        {
            _resources.Remove(resource);
        }

        public IReadOnlyCollection<ResourceBase> GetResources()
        {
            return _resources;
        }

        public bool TryFindNearest(Vector3 position, out ResourceBase nearestResource)
        {
            if (_resources.Count == 0)
            {
                nearestResource = null;
                return false;
            }

            nearestResource = null;
            float minDistance = float.MaxValue;

            foreach (var resource in _resources)
            {
                if (resource.IsAvailable == false)
                {
                    continue;
                }

                float distance = Vector3.Distance(position, resource.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestResource = resource;
                }
            }

            return nearestResource != null;
        }
    }
}
