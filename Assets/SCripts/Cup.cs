using UnityEngine;
using UnityEngine.InputSystem;

public class Cup : MonoBehaviour
{
    [Header("OverlapArea")]
    [SerializeField] Transform _pointA;
    [SerializeField] Transform _pointB;

    [Header("Movement")]
    [SerializeField] private float _MoveSeconds;
    [SerializeField] private Rigidbody2D _rb1;
    private Vector3 _mouseWorldPosition;
    private Vector3 _newPosition;
    private Vector3 _refMoveVelocity;

    private Camera _mainCamera;

    [SerializeField]  private float rotationSpeed;
    private float _initialRotation;
    private float _newRotation;

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
            _rb1.gravityScale = -1 * _rb1.gravityScale;
        }
    }

    private bool _delivered;
    public bool Delivered
    {
        get
        {
            return _delivered;
        }
        set
        {
            _delivered = value;
            _rb1.simulated = false;
        }
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _initialRotation = _rb1.rotation;
    }

    public int CountFluid()
    {
        var colliders = Physics2D.OverlapAreaAll(_pointA.position, _pointB.position, LayerMask.NameToLayer("Liquids"));
        return colliders.Length;
    }

    public void ResetCup()
    {
        _delivered = false;
        _follow = false;
        _rb1.simulated = true;
    }
    public void SetPosition(Transform point)
    {
        _rb1.transform.position = point.position;
    }

    private void Update()
    {
        if (_delivered)
            return;

        if(!_follow)
            return;

        _mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _newPosition = Vector3.SmoothDamp(_rb1.transform.position, _mouseWorldPosition, ref _refMoveVelocity, _MoveSeconds * Time.deltaTime);
        //if(Vector2.Distance(_newPosition, _mouseWorldPosition) <= 0.2f)
        //{
        //    _newPosition = _mouseWorldPosition;
        //}

        float currentRotation = _rb1.rotation;
        if (Mathf.Abs(Mathf.DeltaAngle(currentRotation, _initialRotation)) > 0.1f)
        {
            _newRotation = Mathf.LerpAngle(currentRotation, _initialRotation, Time.deltaTime * rotationSpeed);
        }

    }
    private void FixedUpdate()
    {
        if (!_follow)
            return;

        _rb1.MovePosition(_newPosition);
        _rb1.MoveRotation(_newRotation);
        //_rb1.angularVelocity = Mathf.Clamp(_rb1.angularVelocity,0, 10f);
        //_rb1.totalForce
    }
}

