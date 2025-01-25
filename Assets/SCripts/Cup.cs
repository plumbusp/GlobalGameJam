using UnityEngine;
using UnityEngine.InputSystem;

public class Cup : MonoBehaviour
{
    [SerializeField] private float _MoveSpeed;
    [SerializeField] private Rigidbody2D _rb1;
    [SerializeField] private Rigidbody2D _rb2;
    private Vector3 _mouseWorldPosition;
    private Vector3 _position;
    private Camera _mainCamera;
    [SerializeField]  private float rotationSpeed;
    private float _initialRotation;

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
            _rb1.freezeRotation = !_rb1.freezeRotation;
            //Debug.Log("Gravity scale " + _rb1.gravityScale);
        }
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _initialRotation = _rb1.rotation;
    }

    private void Update()
    {
        if(!_follow)
            return;

        Debug.Log("Following");
        _mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _position = Vector3.Slerp(_rb1.transform.position, _mouseWorldPosition, _MoveSpeed * Time.deltaTime);

        //var angle = Vector3.SignedAngle(_rb1.transform.rotation.eulerAngles, _initialRotation.eulerAngles, Vector3.forward);
        //var angle = _rb1.rotation - _initialRotation.
        //if (angle >= 0.5f)
        //{
        //    _rb1.rotation
        //}

        float currentRotation = _rb1.rotation;

        if (Mathf.Abs(Mathf.DeltaAngle(currentRotation, _initialRotation)) > 0.1f)
        {
            float newRotation = Mathf.LerpAngle(currentRotation, _initialRotation, Time.deltaTime * rotationSpeed);
            _rb1.MoveRotation(newRotation);
        }

    }
    private void FixedUpdate()
    {
        if (!_follow)
            return;

        _rb1.MovePosition(_position);
    }
}

