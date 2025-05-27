using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DronesTest.Gameplay.Faction
{
    public class FactionBase : MonoBehaviour
    {
        [SerializeField] private Factions _factionType;
        [SerializeField] private List<DronePort> _dronePorts = new();

        [SerializeField] private int _resourceCount = 0;

        public event UnityAction<int> ResourceCountChanged;

        public IReadOnlyList<DronePort> DronePorts => _dronePorts;

        public bool TryGetDronePort(out DronePort outDronePort)
        {
            foreach (var dronePort in _dronePorts)
            {
                if (dronePort.IsAvailable)
                {
                    outDronePort = dronePort;
                    return true;
                }
            }

            outDronePort = null;
            return false;
        }

        public void ReceiveResource()
        {
            _resourceCount++;
            ResourceCountChanged?.Invoke(_resourceCount);
        }
    }
}
