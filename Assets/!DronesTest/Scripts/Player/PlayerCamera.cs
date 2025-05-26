using UnityEngine;
using DronesTest.Input;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private InputReader _input;

    [SerializeField] private float _dragSpeed = 0.1f;

    private bool _isDragging;

    private void OnEnable()
    {
        _input.DragEvent += OnDrag;
        _input.DragHoldButtonPerformedEvent += StartDragging;
        _input.DragHoldButtonCanceledEvent += StopDragging;
    }

    private void OnDisable()
    {
        _input.DragEvent -= OnDrag;
        _input.DragHoldButtonPerformedEvent -= StartDragging;
        _input.DragHoldButtonCanceledEvent -= StopDragging;
    }

    private void OnDrag(Vector2 delta)
    {
        if (_isDragging == false)
            return;

        Vector3 move = _dragSpeed * Time.deltaTime * new Vector3(-delta.x, 0, 0);
        transform.position += move;
    }

    private void StartDragging()
    {
        _isDragging = true;
    }

    private void StopDragging()
    {
        _isDragging = false;
    }
}
