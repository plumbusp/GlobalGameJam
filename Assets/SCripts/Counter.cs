using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private Cup _cup;
    [SerializeField] private Cursor _cursor;
    [SerializeField] private Transform _orderTakePoint;

    private void Awake()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GlassBottom"))
        {
            // Check if allowed
            if (!_cup.Follow)
                return;

            //If yes set to right position
            _cup.Delivered = true;
            _cup.Follow = false;
            _cup.SetPosition(_orderTakePoint);

            // Count
            Debug.Log(_cup.CountFluid());
            // Give results
        }
    }
}
