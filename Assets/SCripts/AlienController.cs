using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienController : MonoBehaviour
{
    [SerializeField] Image satisfactionIndicator;
    [SerializeField] List<Alien> aliens = new List<Alien>();
    [SerializeField] Transform aliensParent;
    [SerializeField] float patienceAmount = 15f;
    //[SerializeField] float startDialogDelay = 0.4f;

    private Alien currentAlien;
    private OrderController _orderController;
    private Counter _counter;

    public void Initialize(Counter counter, OrderController orderController)
    {
        _counter = counter;
        _orderController = orderController;
    }

    public Alien SpawnAlien()
    {
        currentAlien = Instantiate(aliens[Random.Range(0, aliens.Count)], aliensParent);
        currentAlien.Initialize(_counter, _orderController, satisfactionIndicator, patienceAmount);
        return currentAlien;
        //StartCoroutine(StartDialogAfterDelay());
    }

    //public void UnspawnAlien()
    //{
    //    currentAlien.GetComponent<Animator>().SetTrigger("Exit");
    //    orderController.CloseBubble();
    //    StartCoroutine(DestroyAlienAfterDelay());
    //}

    //private IEnumerator StartDialogAfterDelay()
    //{
    //    yield return new WaitForSeconds(startDialogDelay);
    //    orderController.CreateRandomOrder();
    //}

    //private IEnumerator DestroyAlienAfterDelay()
    //{
    //    yield return new WaitForSeconds(1f);
    //    Destroy(currentAlien);
    //}
}
