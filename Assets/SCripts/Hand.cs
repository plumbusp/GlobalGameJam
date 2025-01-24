using UnityEngine;

public class Hand : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector3 _mouseWorldPosition;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = _mouseWorldPosition;
    }
}
