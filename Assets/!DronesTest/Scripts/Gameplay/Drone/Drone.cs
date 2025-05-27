using DronesTest.Gameplay.Faction;
using UnityEngine;
using Zenject;

namespace DronesTest.Gameplay.Drone
{
	public class Drone : MonoBehaviour, IDroneResourceHandler
    {
		[SerializeField] private Factions _factionType;
        [SerializeField] private DroneMovement _movement;

        private IResourceFinder _resourceFinder;
        private DroneStateHandler _stateHandler;
        private FactionBase _factionBase;
        private bool _hasResource;

        public FactionBase FactionBase => _factionBase; 

        [Inject]
        private void Construct(IResourceFinder resourceFinder)
        {
            _resourceFinder = resourceFinder;
        }

        public void Init(FactionBase factionBase)
        {
            _factionBase = factionBase;
        }

        private void Awake()
        {
            _stateHandler = new(this, this, _movement);
        }

        private void OnEnable()
        {
            _stateHandler.SetState(State.SearchingResource);
        }

        private void Update()
        {
            _stateHandler.Update();
        }

        public bool TryFindNearestResource(out ResourceBase nearestResource)
        {
            if (_resourceFinder.TryFindNearest(transform.position, out nearestResource))
            {
                nearestResource.Tag();
                return true;
            }

            return false;
        }

        public void CollectResource(ResourceBase resource)
        {
            if (resource.IsCollected)
            {
                return;
            }

            _hasResource = true;
            resource.OnCollect();
            resource.ReturnToPool();
        }

        public void DropOffResource()
        {
            if (_hasResource == false)
            {
                return;
            }

            _hasResource = false;
            _factionBase.ReceiveResource();
        }
    }
}