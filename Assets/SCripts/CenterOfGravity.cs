using UnityEngine;

public class CenterOfGravity : MonoBehaviour
{
    [SerializeField] private Transform _centerOfGravity;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _centerOfGravity.position = new Vector2 (_centerOfGravity.position.x, _centerOfGravity.position.y);
        rb.centerOfMass = _centerOfGravity.position;
    }
}
