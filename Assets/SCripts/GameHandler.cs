using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    //References
    private AlienController alienController;
    private OrderController orderController;

    [Header("References")]
    [SerializeField] Counter counter;
    [SerializeField] Cup cup;

    [Header("Game variables")]
    //[SerializeField] int amountOfCustomers = 8;
    //[SerializeField] float delayBetweenCustomers = 1f;

    private Alien _currentAlien;

    [Header("UI elements")]


    //private float score;
    private float currentStars = 0f;
    //private int currentAlien = 0;

    private IEnumerator patienceTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        alienController = GetComponent<AlienController>();
        orderController = GetComponent<OrderController>();

        alienController.Initialize(counter, orderController);
        counter.OnOrderSubmitted += HandleOrderSubmitted;

        SpawnNewCustomer();
    }
    private void OnDestroy()
    {
        counter.OnOrderSubmitted -= HandleOrderSubmitted;
    }

    private void SpawnNewCustomer()
    {
        cup.Respawn();
        if (_currentAlien != null)
            _currentAlien.OnLeavedWithNoOrder -= HandleCustomerLeaved;
        _currentAlien = alienController.SpawnAlien();

        _currentAlien.OnLeavedWithNoOrder += HandleCustomerLeaved;
    }

    private void HandleCustomerLeaved()
    {
        SpawnNewCustomer();
    }

    private void HandleOrderSubmitted()
    {
        if(_currentAlien.CanGetDrink)
        {
            cup.Delivered = true;
            _currentAlien.GetOrderAndWait();
            orderController.CalculateCurrentScore(_currentAlien, cup);
            orderController.CanMove += AllowCustomerToLeave;
        }
    }

    private void AllowCustomerToLeave()
    {
        orderController.CloseBubble();
        orderController.CanMove -= AllowCustomerToLeave;
        _currentAlien.Leave();
        SpawnNewCustomer();
    }

    //private void StartGame()
    //{
    //    StartCoroutine(CallNextCustomerAfterDelay());
    //}

    //public void AddToScore()
    //{
    //    score = CalculateScore();
    //    Debug.Log(score);
    //}

    //public void CustomerLeaves()
    //{
    //    StopAllCoroutines();
    //    alienController.UnspawnAlien();

    //    StartCoroutine(CallNextCustomerAfterDelay());
    //    satisfactionIndicator.gameObject.SetActive(false);
    //}

    //private float CalculateScore()
    //{
    //    float newScore = score + (currentStars / amountOfCustomers);
    //    return newScore;
    //}

    //private IEnumerator CallNextCustomerAfterDelay()
    //{
    //    if (currentAlien < amountOfCustomers)
    //    {
    //        yield return new WaitForSeconds(delayBetweenCustomers);
    //        alienController.SpawnAlien();
    //        StartCoroutine (PatienceTimer());

    //        currentAlien++;
    //    }
    //}

    //private IEnumerator PatienceTimer()
    //{
    //    yield return new WaitForSeconds(0.4f);
    //    Debug.Log("patience at 3 stars");
    //    currentStars = 3f;
    //    satisfactionIndicator.sprite = satisfactionHappy;
    //    satisfactionIndicator.gameObject.SetActive(true);

    //    float delay = customerPatienceAmount / 3f;

    //    yield return new WaitForSeconds(delay);
    //    Debug.Log("patience at 2 stars");
    //    //change indicator sprite
    //    satisfactionIndicator.sprite = satisfactionMeh;
    //    currentStars = 2f;

    //    yield return new WaitForSeconds(delay);
    //    Debug.Log("patience at 1 stars");
    //    //change indicator sprite
    //    satisfactionIndicator.sprite = satisfactionAngry;
    //    currentStars = 1f;

    //    yield return new WaitForSeconds(delay);
    //    Debug.Log("patience at 0 stars");
    //    currentStars = 0f;
    //    AddToScore();
    //    alienController.UnspawnAlien();
    //    satisfactionIndicator.gameObject.SetActive(false);

    //    StartCoroutine(CallNextCustomerAfterDelay());
    //}
}
