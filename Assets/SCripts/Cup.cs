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
            if(_follow == true)
            {
                _rb1.gravityScale = -Mathf.Abs(_rb1.gravityScale);
            }
            else
            {
                _rb1.gravityScale = Mathf.Abs(_rb1.gravityScale);
            }
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

    /// <summary>
    /// Returns count of liquid particles
    /// </summary>
    /// <returns></returns>
    public void CountContents(out int particlesAmount, out int liquidsUsed, out int primarLiquidID)
    {
        int id_1_particles = 0;
        int id_2_particles = 0;
        int id_3_particles = 0;
        int liquidUsedInner = 0;

        int particleID;

        Debug.Log(LayerMask.LayerToName(6));
        int layerMask = LayerMask.GetMask("Liquids");
        var colliders = Physics2D.OverlapAreaAll(_pointA.position, _pointB.position, layerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].gameObject.SetActive(false);
            particleID = colliders[i].GetComponent<FluidParticle>().ID;

            switch (particleID)
            {
                case 1: id_1_particles++; break;
                case 2: id_2_particles++; break;
                case 3: id_3_particles++; break;
            }
        }
        if (id_1_particles > 1)
            liquidUsedInner++;
        if (id_2_particles > 1)
            liquidUsedInner++;
        if (id_3_particles > 1)
            liquidUsedInner++;

        liquidsUsed = liquidUsedInner;
        primarLiquidID = CountPrimarLiwuidID(id_1_particles, id_2_particles, id_3_particles);
        particlesAmount = colliders.Length;

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

    private int CountPrimarLiwuidID(int ID1, int ID2, int ID3)
    {
        if (ID1 > ID2 || ID1 > ID3)
            return 1;
        if (ID2 > ID1 || ID2 > ID3)
            return 2;
        if (ID3 > ID1 || ID3 > ID2)
            return 3;
        else
            return UnityEngine.Random.Range(1,4);
    }
}

