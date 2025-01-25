using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    [SerializeField] List<GameObject> aliens = new List<GameObject>();
    [SerializeField] Transform aliensParent;
    [SerializeField] float startDialogDelay = 0.4f;

    [Header("References")]
    [SerializeField] OrderController orderController;

    private GameObject currentAlien;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnAlien();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentAlien.GetComponent<Animator>().SetTrigger("Exit");
            orderController.CloseBubble();
            StartCoroutine(DestroyAlienAfterDelay());

        }
    }

    private void SpawnAlien()
    {
        currentAlien = Instantiate(aliens[Random.Range(0, aliens.Count)],aliensParent);
        StartCoroutine(StartDialogAfterDelay());
    }

    private IEnumerator StartDialogAfterDelay()
    {
        yield return new WaitForSeconds(startDialogDelay);
        orderController.RandomizeOrder();
    }

    private IEnumerator DestroyAlienAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        Destroy(currentAlien);
    }
}
