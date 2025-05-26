using System.Collections.Generic;
using UnityEngine;

namespace DronesTest.Gameplay
{
    public class ResourceManager : IResourceFinder, IResourceProvider
    {
        private HashSet<ResourceBase> _resources = new();

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

        public ResourceBase FindNearest(Vector3 position)
        {
            ResourceBase nearest = null;

            float minDistance = float.MaxValue;

            foreach (var resource in _resources)
            {
                float distance = Vector3.Distance(position, resource.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = resource;
                }
            }

            return nearest;
        }
    }
}
