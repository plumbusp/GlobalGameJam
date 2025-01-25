using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
    [SerializeField] private Cup _cup;
    private InputActions _actions;

    private void Start()
    {
        _actions = new InputActions();
        _actions.Player.Enable();
        _actions.Player.Click.started += SnapCup;
        _actions.Player.Click.canceled += UnsnapCup;
    }

    private void SnapCup(InputAction.CallbackContext context)
    {
        _cup.Follow = true;
    }

    private void UnsnapCup(InputAction.CallbackContext context)
    {
        _cup.Follow = false;
    }
}
