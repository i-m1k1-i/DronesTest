using UnityEngine;

namespace DronesTest.Gameplay
{
    public interface IDroneResourceHandler
    {
        bool TryFindNearestResource(out ResourceBase nearestResource);
        void CollectResource(ResourceBase resource);
        void DropOffResource();
    }
}
