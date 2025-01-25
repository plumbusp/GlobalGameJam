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
            _cursor.OnCupUnsnaped += HandleCupDelivered;
            //Check if allowed
            //if (!_cup.Follow)
            //        return;

            //If yes set to right position
            //_cup.Delivered = true;
            //_cup.Follow = false;
            //_cup.SetPosition(_orderTakePoint);

            //Count
            //Debug.Log(_cup.CountFluid());
            //Give results
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("GlassBottom"))
        {
            _cursor.OnCupUnsnaped -= HandleCupDelivered;
        }
    }
    private void OnDestroy()
    {
        _cursor.OnCupUnsnaped -= HandleCupDelivered;
    }

    private void HandleCupDelivered()
    {
        // count staff
        int i;
        int y;
        int x;
        int z;
        _cup.CountContents(out i,out y, out x, out z);
        Debug.Log($" {i} {y}  {x}  {z}");
        _cup.Delivered = true;
        //Set cup to false
    }
}
