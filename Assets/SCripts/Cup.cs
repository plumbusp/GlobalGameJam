using UnityEngine;

public class Cup : MonoBehaviour
{
    [SerializeField] private float _MoveSpeed;
    private Vector3 _mouseWorldPosition;
    private Vector3 _position;
    private Camera _mainCamera;
    private Rigidbody2D _rb;

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
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if(!_follow)
            return;

        _mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _position = Vector3.Lerp(transform.position, _mouseWorldPosition, _MoveSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        _rb.MovePosition(_position);
    }
}

