using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TutorialContent : MonoBehaviour
{
    [SerializeField] private TMP_Text _TextField;
    [SerializeField] private float _PrintingSpeed;

    [SerializeField] GameObject alien;
    [SerializeField] GameObject flavourPaper;
    [SerializeField] GameObject cup;

    private string _text;
    private WaitForSeconds _printingSeconds;
    private bool _skip;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _printingSeconds = new WaitForSeconds(_PrintingSpeed);
        _text = _TextField.text;
    }

    private IEnumerator TypeText(string text)
    {
        _TextField.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            // IF skip
            // skip
            if (_skip)
                yield break;

            _TextField.text += text[i];
            yield return _printingSeconds;
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
        if (alien != null)
        {
            alien.SetActive(true);
        }
        if (flavourPaper != null)
        {
            flavourPaper.SetActive(true);
        }
        if (cup != null)
        {
            cup.SetActive(true);
        }
        _skip = false;
        _animator.SetTrigger("Appear");
        StartCoroutine(TypeText(_text));
    }

    public void Skip()
    {
        _skip = true;
        gameObject.SetActive(false);
    }
}
