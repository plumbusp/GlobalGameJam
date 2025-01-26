using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Alien : MonoBehaviour
{
    public event Action OnReadyToOrder;
    public event Action OnLeaved;

    [Header("Satisfaction Indicators")]
    [SerializeField] Sprite satisfactionHappy;
    [SerializeField] Sprite satisfactionMeh;
    [SerializeField] Sprite satisfactionAngry;
    private Image _satisfactionIndicator;

    [Header("Speach Sound")]
    [SerializeField] private SFXType speachSound;

    private Counter _counter;
    private OrderController _orderController;
    private Animator _animator;
    private int _desiredFlavourID;

    private Coroutine _currentCoroutine;
    private float _patienceAmount;
    private int currentStars;

    public void Initialize(Counter counter, OrderController orderController, Image satisfactionIndicator, float patienceAmount)
    {
        _animator = GetComponent<Animator>();
        _counter = counter;
        _orderController = orderController;
        _patienceAmount = patienceAmount;

        _orderController.CreateRandomOrder(out _desiredFlavourID);
        currentStars = 3;
        _animator.SetTrigger("Appear");
    }

    public void Wait()
    {
        _counter.On
        if(_currentCoroutine != null)
            StopAllCoroutines();

        _currentCoroutine = StartCoroutine(PatienceTimer());
    }

    private IEnumerator PatienceTimer()
    {
        Debug.Log("patience at 3 stars");

        _satisfactionIndicator.gameObject.SetActive(true);
        _satisfactionIndicator.sprite = satisfactionHappy;
        currentStars = 3;
        float delay = _patienceAmount / 3f;
        yield return new WaitForSeconds(delay);

        Debug.Log("patience at 2 stars");
        _satisfactionIndicator.sprite = satisfactionMeh;
        currentStars = 2;
        yield return new WaitForSeconds(delay);

        Debug.Log("patience at 1 stars");
        _satisfactionIndicator.sprite = satisfactionAngry;
        currentStars = 1;

        yield return new WaitForSeconds(delay);
        Debug.Log("patience at 0 stars");
        currentStars = 0;

        _orderController.AddToScore(currentStars);
        _satisfactionIndicator.gameObject.SetActive(false);
        _animator.SetTrigger("Exit");
    }


}
