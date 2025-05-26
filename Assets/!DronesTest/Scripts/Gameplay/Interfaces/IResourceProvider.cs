using System.Collections.Generic;

namespace DronesTest.Gameplay
{
    public interface IResourceProvider
    {
        void Register(ResourceBase resource);
        void Unregister(ResourceBase resource);

        IReadOnlyCollection<ResourceBase> GetResources();
    }
}
