using UnityEngine;

public class FluidParticle : MonoBehaviour
{
    public void SetFall(Vector3 fallPos) 
    { 
        transform.position = fallPos;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("FluidDestroyer"))
            gameObject.SetActive(false);
	}
}
