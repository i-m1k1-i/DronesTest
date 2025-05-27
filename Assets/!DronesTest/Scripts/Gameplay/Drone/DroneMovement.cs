using UnityEngine;

namespace DronesTest.Gameplay.Drone
{
    public class DroneMovement : MonoBehaviour, IDroneMovement
    {
        [SerializeField] float _avoidStrength = 1.5f;
        [SerializeField] float _speed = 5f;

        private Transform _droneTransform;
        private Transform _destination;
        private Vector3 _avoid = Vector3.zero;

        private bool _isMoving = false;

        public bool IsMoving => _isMoving;

        private void Awake()
        {
            _droneTransform = transform.parent;
        }

        private void Update()
        {
            if (_isMoving == false)
                return;

            MoveToDestination();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Drone>(out var drone))
            {
                Debug.Log("avoiding");
                Vector3 dirToDrone = (drone.transform.position - _droneTransform.position).normalized;
                _avoid -= dirToDrone * _avoidStrength;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Drone>(out var drone))
            {
                _avoid = Vector3.zero;
            }
        }

        public void SetDestination(Transform destination)
        {
            _destination = destination;
            _isMoving = true;
        }

        public void Stop()
        {
            _isMoving = false;
            _destination = null;
            _avoid = Vector3.zero;
        }

        private void MoveToDestination()
        {
            Debug.Log("Moving");
            Vector3 targetDirection = ((_destination.position - _droneTransform.position) + _avoid).normalized;

            _droneTransform.rotation = Quaternion.Slerp(_droneTransform.rotation,
                Quaternion.LookRotation(targetDirection),
                Time.deltaTime * 5f);

            _droneTransform.position += targetDirection * _speed * Time.deltaTime;
        }
    }
}