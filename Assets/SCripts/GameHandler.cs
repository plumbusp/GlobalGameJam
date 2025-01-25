using System.Collections;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    //References
    private AlienController alienController;
    private OrderController orderController;

    [Header("Game variables")]
    [SerializeField] int amountOfCustomers = 8;
    [SerializeField] float delayBetweenCustomers = 1f;
    [SerializeField] float customerPatienceAmount = 15f;


    private float score;
    private float currentStars = 0f;
    private int currentAlien = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        alienController = GetComponent<AlienController>();
        orderController = GetComponent<OrderController>();

        StartGame();
    }

    private void StartGame()
    {
        StartCoroutine(CallNextCustomerAfterDelay());
    }

    public void AddToScore()
    {
        score = CalculateScore();
    }

    public void CustomerLeaves()
    {
        StopCoroutine(PatienceTimer());
        alienController.UnspawnAlien();

        StartCoroutine(CallNextCustomerAfterDelay());
    }

    private float CalculateScore()
    {
        float newScore = score + (currentStars / amountOfCustomers);
        return newScore;
    }

    private IEnumerator CallNextCustomerAfterDelay()
    {
        if (currentAlien < amountOfCustomers)
        {
            yield return new WaitForSeconds(delayBetweenCustomers);
            alienController.SpawnAlien();
            StartCoroutine (PatienceTimer());

            currentAlien++;
        }
    }

    private IEnumerator PatienceTimer()
    {
        Debug.Log("patience at 3 stars");
        currentStars = 3f;

        float delay = customerPatienceAmount / 3f;

        yield return new WaitForSeconds(delay);
        Debug.Log("patience at 2 stars");
        //change indicator sprite
        currentStars = 2f;

        yield return new WaitForSeconds(delay);
        Debug.Log("patience at 1 stars");
        //change indicator sprite
        currentStars = 1f;

        yield return new WaitForSeconds(delay);
        Debug.Log("patience at 0 stars");
        currentStars = 0f;
        AddToScore();
        alienController.UnspawnAlien();

        StartCoroutine(CallNextCustomerAfterDelay());
    }
}
