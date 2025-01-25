using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private Cup _cup;
    [SerializeField] private Transform _orderTakePoint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GlassBottom"))
        {
            // Check if allowed
            if (!_cup.Follow)
                return;
            _cup.Delivered = true;
            _cup.Follow = false;
            _cup.SetPosition(_orderTakePoint);
            //If yes set to right position
            // Count
            // Give results
        }
    }
}
