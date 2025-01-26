using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;

public class OrderController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextController textController;

    [Header("UI elements")]
    [SerializeField] Transform textBubble;
    [SerializeField] TMP_Text text;

    [Header("Voice lines")]
    [SerializeField] List<string> flavour1Lines = new List<string>();
    [SerializeField] List<string> flavour2Lines = new List<string>();
    [SerializeField] List<string> flavour3Lines = new List<string>();


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
    public void CreateRandomOrder(out int flavourID)
    {
        currentAlien++;
        Debug.Log(" Current Alien + " + currentAlien);
        currentFlavour = Random.Range(1, 4);
        flavourID = currentFlavour;

        switch (currentFlavour)
        {
            case 1:
                currentVoiceLine = flavour1Lines[Random.Range(0, flavour1Lines.Count)];
                flavourPaper1.gameObject.SetActive(true);
                break;
            case 2:
                currentVoiceLine = flavour2Lines[Random.Range(0, flavour1Lines.Count)];
                flavourPaper2.gameObject.SetActive(true);
                break;
            case 3:
                currentVoiceLine = flavour3Lines[Random.Range(0, flavour1Lines.Count)];
                flavourPaper3.gameObject.SetActive(true);
                break;
            default:
                Debug.LogWarning("You are not suppoused to be here!!!");
                break;
        }
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

    public void AddToScore(int currentStars)
    {
        float newTotalScore = totalScore + (currentStars / currentAlien);
        Debug.Log(newTotalScore);
    }
}
