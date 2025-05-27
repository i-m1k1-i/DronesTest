using DronesTest.Gameplay.Faction;
using UnityEngine;
using Zenject;

namespace DronesTest.Gameplay.Drone
{
    public class DronesManager : MonoBehaviour
    {
        [SerializeField] private Drone _redDronePrefab;
        [SerializeField] private Drone _blueDronePrefab;

        [SerializeField] private FactionBase _redFactionBase;
        [SerializeField] private FactionBase _blueFactionBase;

        [SerializeField, Range(1, 10)] private uint _dronesForFaction = 5;

        private DiContainer _container;

        [Inject]
        private void Consturct(DiContainer container)
        {
            _container = container;
        }

        private void Start()
        {
            SpawnDrones();
        }

        private void SpawnDrones()
        {
            for (int i = 0; i < _dronesForFaction; i++)
            {
                var bluePort = _blueFactionBase.DronePorts[i];
                var redPort = _redFactionBase.DronePorts[i];

                Drone blueDrone = _container.InstantiatePrefabForComponent<Drone>(
                    _blueDronePrefab, 
                    bluePort.transform.position, 
                    Quaternion.identity, null);

                Drone redDrone = _container.InstantiatePrefabForComponent<Drone>(
                    _redDronePrefab,
                    redPort.transform.position,
                    Quaternion.identity, null);


                blueDrone.Init(_blueFactionBase);
                redDrone.Init(_redFactionBase);
            }
        }
    }
}
