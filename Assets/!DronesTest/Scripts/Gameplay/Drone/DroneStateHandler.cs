using DronesTest.Gameplay.Faction;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace DronesTest.Gameplay.Drone
{
    public enum State
    {
        SearchingResource,
        MovingToResource,
        Collecting,
        ReturningToBase
    }

    public class DroneStateHandler
    {
        private const float CollectDelay = 2f;

        private readonly Drone _drone;
        private readonly Transform _transform;
        private readonly IDroneResourceHandler _resourceHandler;
        private readonly IDroneMovement _droneMovement;

        private ResourceBase _targetResource;
        private DronePort _targetPort;
        private State _currentState = State.SearchingResource;
        private bool _isCollecting = false;
        private float _startCollectingTime;

        private FactionBase FactionBase => _drone.FactionBase;

        public DroneStateHandler(Drone drone, IDroneResourceHandler resourceHandler, IDroneMovement droneMovement)
        {
            _drone = drone;
            _transform = drone.transform;
            _resourceHandler = resourceHandler;
            _droneMovement = droneMovement;
        }

        public void Update()
        {
            switch (_currentState)
            {
                case State.SearchingResource:
                    SearchResource();
                    break;

                case State.MovingToResource:
                    MoveToResource();
                    break;

                case State.Collecting:
                    Collect();
                    break;

                case State.ReturningToBase:
                    ReturnToBase();
                    break;
            }
        }

        public void SetState(State newState)
        {
            if (_currentState == newState)
                return;

            _currentState = newState;
        }

        private void SearchResource()
        {
            if (_resourceHandler.TryFindNearestResource(out _targetResource))
            {
                SetState(State.MovingToResource);
                Debug.Log("Found resource: " + _targetResource.name);
            }
        }

        private void MoveToResource()
        {
            if (_droneMovement.IsMoving == false)
            {
                _droneMovement.SetDestination(_targetResource.transform);
                Debug.Log("Moving to resource: " + _targetResource.name);
            }

            if (Vector3.Distance(_transform.position, _targetResource.transform.position) <= 0.1f)
            {
                SetState(State.Collecting);
                _droneMovement.Stop();
            }
        }

        private void Collect()
        {
            if (_isCollecting == false)
            {
                _startCollectingTime = Time.timeSinceLevelLoad;
                _isCollecting = true;
            }

            if (Time.timeSinceLevelLoad - _startCollectingTime < CollectDelay)
                return;

            _isCollecting = false;
            _resourceHandler.CollectResource(_targetResource);
            SetState(State.ReturningToBase);
        }

        private void ReturnToBase()
        {
            if (_droneMovement.IsMoving == false)
            {
                if (FactionBase.TryGetDronePort(out _targetPort) == false)
                {
                    Debug.Log("No available port found for returning resource.");
                    return;
                }

                Debug.Log("Moving to port: " + _targetPort.name);
                _targetPort.IsAvailable = false;
                _droneMovement.SetDestination(_targetPort.transform); 
            }

            if (Vector3.Distance(_transform.position, _targetPort.Position) <= 0.1f)
            {
                Debug.Log("Returning resource to port");
                _targetPort.IsAvailable = true;
                _targetPort = null;

                _resourceHandler.DropOffResource();

                SetState(State.SearchingResource);
                _droneMovement.Stop();
            }
        }
    }
}
