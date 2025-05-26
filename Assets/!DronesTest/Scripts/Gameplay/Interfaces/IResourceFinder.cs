using UnityEngine;

namespace DronesTest.Gameplay
{
    public interface IResourceFinder
    {
        ResourceBase FindNearest(Vector3 position);
    }
}
