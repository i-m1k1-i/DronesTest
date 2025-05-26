using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace DronesTest.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObjects/InputReader")]
    public class InputReader : ScriptableObject, GameInput.IPlayerActions
	{
        private GameInput _gameInput;

        public event UnityAction<Vector2> DragEvent;
        public event UnityAction DragHoldButtonPerformedEvent;
        public event UnityAction DragHoldButtonCanceledEvent;
        public event UnityAction ClickEvent;

        private void OnEnable()
        {
            _gameInput ??= new GameInput();

            _gameInput.Player.SetCallbacks(this);
            _gameInput.Enable();
        }

        private void OnDisable()
        {
            _gameInput.Disable();
        }

        public void OnDrag(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Vector2 delta = context.ReadValue<Vector2>();
                DragEvent?.Invoke(delta);
            }
        }

        public void OnDragHoldButton(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                DragHoldButtonPerformedEvent?.Invoke();
            }
            else if (context.canceled)
            {
                DragHoldButtonCanceledEvent?.Invoke();
            }
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                ClickEvent?.Invoke();
            }
        }
	}
}