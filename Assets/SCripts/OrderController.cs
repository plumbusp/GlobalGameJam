using UnityEngine;
using TMPro;
using System.Collections.Generic;

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

    private int currentFlavour = 1;
    private string currentVoiceLine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textController.Initialize(text);
    }

    public void RandomizeOrder()
    {
        currentFlavour = Random.Range(1, 3);

        switch (currentFlavour)
        {
            case 1:
                currentVoiceLine = flavour1Lines[Random.Range(0, flavour1Lines.Count - 1)];
                break;
            case 2:
                currentVoiceLine = flavour2Lines[Random.Range(0, flavour1Lines.Count - 1)];
                break;
            case 3:
                currentVoiceLine = flavour3Lines[Random.Range(0, flavour1Lines.Count - 1)];
                break;
        }
        StartTextBubble();
    }

    public void StartTextBubble()
    {
        textBubble.gameObject.SetActive(true);
        textController.StartDialog(currentVoiceLine);
    }

    public void CloseBubble()
    {
        textBubble.gameObject.SetActive(false);
    }
}
