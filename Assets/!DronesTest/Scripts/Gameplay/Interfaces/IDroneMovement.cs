using UnityEngine;

namespace DronesTest.Gameplay
{
    public interface IDroneMovement
    {
        bool IsMoving { get; }

        void SetDestination(Transform destination);
        void Stop();
    }
}
