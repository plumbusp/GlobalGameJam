using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Alien : MonoBehaviour
{
    public event Action OnReadyToOrder;
    public event Action OnLeavedWithNoOrder;

    [Header("Satisfaction Indicators")]
    [SerializeField] Sprite satisfactionHappy;
    [SerializeField] Sprite satisfactionMeh;
    [SerializeField] Sprite satisfactionAngry;
    private Image _satisfactionIndicator;
    private OrderController _orderController;

    [Header("Speach Sound")]
    [SerializeField] private SFXType speachSound;

    private Animator _animator;

    private Coroutine _currentCoroutine;
    private float _patienceAmount;
    public int DesiredFlavourID { get; private set; }
    public int CurrentStars {  get; private set; }
    public bool CanGetDrink{  get; private set; }

    public void Initialize(OrderController orderController, int desiredFlavourID, Image satisfactionIndicator, float patienceAmount)
    {
        _animator = GetComponent<Animator>();

        _patienceAmount = patienceAmount;
        DesiredFlavourID = desiredFlavourID;
        _satisfactionIndicator = satisfactionIndicator;
        _orderController = orderController;

        CurrentStars = 3;
        _animator.SetTrigger("Appear");
    }

    public void Wait()
    {
        if(_currentCoroutine != null)
            StopAllCoroutines();

        AudioManager.instance.PlayAudioOver(speachSound);
        _currentCoroutine = StartCoroutine(PatienceTimer());
    }

    public void GetOrderAndWait()
    {
        if (_currentCoroutine != null)
            StopAllCoroutines();
    }

    public void Leave()
    {
        AudioManager.instance.StopAudioOver();
        _animator.SetTrigger("Exit");
        _satisfactionIndicator.gameObject.SetActive(false);
    }

    private IEnumerator PatienceTimer()
    {
        CanGetDrink = true;
        _orderController.StartTextBubble();
        Debug.Log("patience at 3 stars");

        _satisfactionIndicator.gameObject.SetActive(true);
        _satisfactionIndicator.sprite = satisfactionHappy;
        CurrentStars = 3;
        float delay = _patienceAmount / 3f;
        yield return new WaitForSeconds(delay + 3);

        Debug.Log("patience at 2 stars");
        _satisfactionIndicator.gameObject.SetActive(true);
        _satisfactionIndicator.sprite = satisfactionMeh;
        CurrentStars = 2;
        yield return new WaitForSeconds(delay - 3);

        Debug.Log("patience at 1 stars");
        _satisfactionIndicator.sprite = satisfactionAngry;
        CurrentStars = 1;

        yield return new WaitForSeconds(delay);
        Debug.Log("patience at 0 stars");
        CurrentStars = 0;

        _satisfactionIndicator.gameObject.SetActive(false);
        AudioManager.instance.StopAudioOver();
        _animator.SetTrigger("Exit");
        CanGetDrink = false;
        OnLeavedWithNoOrder?.Invoke();
    }


}
