using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using System.Collections;

public class OrderController : MonoBehaviour
{
    public event Action CanMove;

    [Header("Order Settings")]
    [SerializeField] int _goodParticleAmount;
    [SerializeField] int _goodBobaAmount;



    [Header("References")]
    [SerializeField] TextController textController;

    [Header("UI elements")]
    [SerializeField] Transform textBubble;
    [SerializeField] TMP_Text text;

    [Header("Voice lines")]
    [SerializeField] List<string> flavour1Lines = new List<string>();
    [SerializeField] List<string> flavour2Lines = new List<string>();
    [SerializeField] List<string> flavour3Lines = new List<string>();

    [Header("Too little Liquid Line")]
    [SerializeField] private string tooLittleLiquidLine;
    [Header("Too little boba line")]
    [SerializeField] private string tooLittleBobaLine;
    [Header("Different Flavour Line")]
    [SerializeField] private string differentFlavourLine;


    [Header("Flavour Sprites")]
    [SerializeField] Transform flavourPaper1;
    [SerializeField] Transform flavourPaper2;
    [SerializeField] Transform flavourPaper3;

    public int currentFlavour = 1;
    private string currentVoiceLine;

    //Score Paramters
    private float totalScore;
    private int currentAlien = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textController.Initialize(text);
    }

    /// <summary>
    /// Creates an order and returns correcponding flavour ID
    /// </summary>
    /// <returns></returns>
    public int CreateRandomOrder()
    {
        currentAlien++;
        Debug.Log(" Current Alien + " + currentAlien);
        currentFlavour = UnityEngine.Random.Range(1, 4);

        switch (currentFlavour)
        {
            case 1:
                currentVoiceLine = flavour1Lines[UnityEngine.Random.Range(0, flavour1Lines.Count)];
                flavourPaper1.gameObject.SetActive(true);
                break;
            case 2:
                currentVoiceLine = flavour2Lines[UnityEngine.Random.Range(0, flavour1Lines.Count)];
                flavourPaper2.gameObject.SetActive(true);
                break;
            case 3:
                currentVoiceLine = flavour3Lines[UnityEngine.Random.Range(0, flavour1Lines.Count)];
                flavourPaper3.gameObject.SetActive(true);
                break;
            default:
                Debug.LogWarning("You are not suppoused to be here!!!");
                break;
        }
        return currentFlavour;
    }

    public void StartTextBubble()
    {
        textBubble.gameObject.SetActive(true);
        textController.StartDialog(currentVoiceLine);
    }

    public void CloseBubble()
    {
        textBubble.gameObject.SetActive(false);
        flavourPaper1.gameObject.SetActive(false);
        flavourPaper2.gameObject.SetActive(false);
        flavourPaper3.gameObject.SetActive(false);
    }

    public float CalculateCurrentScore(Alien alien, Cup cup)
    {
        textController.Stoptext();
        float particleAmount;
        float liquidsUsed;
        float primaryLiquidID;
        float bobaCount;
        float currentStars = alien.CurrentStars;

        textController.OnSentenceEnded += InVokeCanMove;
        cup.CountContents(out particleAmount,out liquidsUsed,out primaryLiquidID,out bobaCount);

        if(particleAmount <= 3)
        {
            currentStars = 0;
            textController.StartDialog("Are you kidding me? There is nothing in my cup!");
        }

        else if(liquidsUsed > 1 || primaryLiquidID != alien.DesiredFlavourID)
        {
            currentStars -= 1f;
            textController.StartDialog(differentFlavourLine);
        }
        else  if (particleAmount < _goodParticleAmount)
        {
            currentStars -= 1f;
            textController.StartDialog(tooLittleLiquidLine);
        }

        else if(bobaCount < _goodBobaAmount)
        {
            currentStars -= 1f;
            textController.StartDialog(tooLittleBobaLine);
        }
        else
        {
            textController.StartDialog("Thank you!");
        }

        //float newTotalScore = totalScore + (currentStars / currentAlien);
        totalScore += currentStars;
        return totalScore / currentAlien;
    }

    private void InVokeCanMove()
    {
        StartCoroutine(WaitALittleBit());
    }

    IEnumerator WaitALittleBit()
    {
        yield return new WaitForSeconds(1f);
        CanMove?.Invoke();
        textController.OnSentenceEnded -= InVokeCanMove;
    }
}
