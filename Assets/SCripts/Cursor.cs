using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
    public event Action OnCupUnsnaped;
    [SerializeField] private Cup _cup;
    private InputActions _actions;
    Camera _mainCamera;

    private void Start()
    {
        _actions = new InputActions();
        _actions.Player.Enable();
        _actions.Player.Click.started += TrySnapCup;
        _actions.Player.Click.canceled += UnsnapCup;
        _mainCamera = Camera.main;
    }
    private void OnDestroy()
    {
        _actions.Player.Disable();
        _actions.Player.Click.started -= TrySnapCup;
        _actions.Player.Click.canceled -= UnsnapCup;
    }
    private void TrySnapCup(InputAction.CallbackContext context)
    {
        var startPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit hit;

        int layerMask = LayerMask.GetMask("GlassRaycastCatcher");
        if (Physics2D.Raycast(startPoint, Vector3.forward, 30f, layerMask))
        {
            _cup.Follow = true;
        }
    }

    private void UnsnapCup(InputAction.CallbackContext context)
    {
        _cup.Follow = false;
        OnCupUnsnaped?.Invoke();
    }
}
