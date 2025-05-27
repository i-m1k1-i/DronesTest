using UnityEngine;

namespace DronesTest.Gameplay
{
    public interface IResourceFinder
    {
        bool TryFindNearest(Vector3 position, out ResourceBase outResource);
    }
}
