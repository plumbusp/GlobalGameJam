using System;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public event Action<int> OnOrderSubmitted;
    [SerializeField] private Cup _cup;
    [SerializeField] private Cursor _cursor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GlassBottom"))
        {
            _cursor.OnCupUnsnaped += HandleCupDelivered;
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
        OnOrderSubmitted?.Invoke();
        _cursor.OnCupUnsnaped -= HandleCupDelivered;
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
