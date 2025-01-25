using UnityEngine;

public class Cup : MonoBehaviour
{
    [SerializeField] private float _MoveSpeed;
    [SerializeField] private Rigidbody2D _rb1;
    [SerializeField] private Rigidbody2D _rb2;
    private Vector3 _mouseWorldPosition;
    private Vector3 _position;
    private Camera _mainCamera;

    private bool _follow;
    public bool Follow 
    {
        get 
        {
            return _follow;
        }
        set
        {
            _follow = value;
        }
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if(!_follow)
            return;

        Debug.Log("Following");
        _mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _position = Vector3.Lerp(_rb1.transform.position, _mouseWorldPosition, _MoveSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        if (!_follow)
            return;

        _rb1.MovePosition(_position);
    }
}

