using System.Collections;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class FossitButton : MonoBehaviour
{
    [Header("Fossit")]
    [SerializeField] Fossit _correspondingFossit;

    [Header("Visual Parameters")] 
    private SpriteRenderer _spriteRenderer;
    [SerializeField] float _pressedTime;
    private WaitForSeconds _pressedSeconds;

    [SerializeField] Sprite _pressedSprite;
    [SerializeField] private Color _pressedColor;
    [SerializeField] Sprite _normalSprite;
    private Color _normalColor;

    private Coroutine _currentCoroutine;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _normalColor = _spriteRenderer.color;
        _pressedSeconds = new WaitForSeconds(_pressedTime);
    }
    private void OnMouseDown()
    {
        if(!_correspondingFossit.IsOpen)
        {
            AudioManager.instance.PlayAudioTwo(SFXType.PopSound);
            _correspondingFossit.Open();
        }
        else
        {
            _correspondingFossit.Close();
            if (_currentCoroutine != null)
                StopAllCoroutines();
            _currentCoroutine = StartCoroutine(PressButton());
            //AudioManager.instance.StopAudioOverTwo();
        }
        
    }
    private IEnumerator PressButton()
    {
        _spriteRenderer.color = _pressedColor;
        _spriteRenderer.sprite = _pressedSprite;
        yield return _pressedSeconds;
        _spriteRenderer.color = _normalColor;
        _spriteRenderer.sprite = _normalSprite;
    }
}
