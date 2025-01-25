using UnityEngine;

public class FluidParticle : MonoBehaviour
{
    [SerializeField] private float topSpeed;
    private Rigidbody2D rb;
    private bool _isInitialized = false;
    public int ID {  get; set; }

    public void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        _isInitialized = true;
    }
    public void SetFall(Vector3 fallPos) 
    { 
        transform.position = fallPos;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("FluidDestroyer"))
            gameObject.SetActive(false);
	}
    private void Update()
    {
        if (!_isInitialized)
            return;

        if (rb.linearVelocity.magnitude > topSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * topSpeed;
    }
}
