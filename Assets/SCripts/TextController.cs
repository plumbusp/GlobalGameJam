using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [Header("Symbol spawning")]
    [SerializeField] private float _symbolsSpawnRate;
    private WaitForSeconds _waitSymbols;

    private State _SentanceState;
    private TMP_Text _speachText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _waitSymbols = new WaitForSeconds(_symbolsSpawnRate);
    }

    public void Initialize(TMP_Text speachText)
    {
        _speachText = speachText;
    }

    public void StartDialog(string sentence)
    {
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        Debug.Log("Typing...");

        _SentanceState = State.Playing;
        _speachText.text = "";
        int wordIndex = 0;

        while (_SentanceState != State.Completed)
        {
            _speachText.text += sentence[wordIndex];
            yield return _waitSymbols;
            if (++wordIndex == sentence.Length)
            {
                _SentanceState = State.Completed;
                //yield return _waitSentence;
                //OnSentenceCompleted?.Invoke(_sentenceIndex);
                break;
            }
        }
    }

    enum State
    {
        Playing,
        Completed
    }
}
