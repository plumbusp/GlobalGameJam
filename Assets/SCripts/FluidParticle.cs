using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FluidParticle : MonoBehaviour
{
    [SerializeField] private float topSpeed;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool _isInitialized = false;
    public int ID {  get; set; }
    public Color Color { get; set; }

    public void Initialize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        _isInitialized = true;
        spriteRenderer.color = Color;
    }
    public void SetFall(Vector3 fallPos) 
    { 
        transform.position = fallPos;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(name);
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
