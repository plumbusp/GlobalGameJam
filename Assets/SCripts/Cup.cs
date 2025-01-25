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
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _initialRotation = _rb1.rotation;
    }

    public int CountFluid()
    {
        Debug.Log(LayerMask.LayerToName(6));
        int layerMask = LayerMask.GetMask("Liquids");
        var colliders = Physics2D.OverlapAreaAll(_pointA.position, _pointB.position, layerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].gameObject.SetActive(false);
        }
        return colliders.Length;
    }

    public void ResetCup()
    {
        _delivered = false;
        _follow = false;
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

    private void OnDrawGizmos()
    {
        Vector2 bottomLeft = _pointA.position;
        Vector2 topRight = _pointB.position;

        // Calculate the other corners of the rectangle
        Vector2 topLeft = new Vector2(bottomLeft.x, topRight.y);
        Vector2 bottomRight = new Vector2(topRight.x, bottomLeft.y);

        // Draw the rectangle with Gizmos
        Gizmos.color = Color.green;
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
    }
}

