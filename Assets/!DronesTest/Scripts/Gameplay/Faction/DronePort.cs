using UnityEngine;

namespace DronesTest.Gameplay.Faction
{
	public class DronePort : MonoBehaviour
	{
		[SerializeField] private bool _isAvailable = true;
        public Vector3 Position => transform.position;
        public bool IsAvailable 
        { 
            get { return _isAvailable; } 
            set { _isAvailable = value; } 
        }
    }
}