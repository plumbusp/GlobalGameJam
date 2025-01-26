using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
    [SerializeField] GameObject _dayTimeObject;
    [SerializeField] GameObject _inGameStarsObject;

    [SerializeField] Slider timeSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Color sunColor;
    [SerializeField] private Color moonColor;

    [SerializeField] float timeUntilGameEndSec;
    private WaitForSeconds waitUntilEnd;
    float currentTime;
    float t_forLerp;

    [SerializeField] Slider endGameStarSlider;
    [SerializeField] Slider currentStarSlider;
    [SerializeField] GameObject endGamePanel;
    [SerializeField] GameObject pauseMenu;
    private float _currentscore;

    bool gameEnded = false;

    private void Start()
    {
        waitUntilEnd = new WaitForSeconds(timeUntilGameEndSec);
        Time.timeScale = 1.0f;
        currentStarSlider.value = 0;
        endGamePanel.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        StartCoroutine(WaitUntilTheEnd());

        timeSlider.maxValue = timeUntilGameEndSec;
        Debug.Log("timeSlider.maxValue " + timeSlider.maxValue);
        timeSlider.value = 0;
    }
    void Update()
    {
        if (gameEnded)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(0);
            }
            return;
        }
        currentTime += Time.deltaTime;
        t_forLerp = currentTime / timeUntilGameEndSec;
        timeSlider.value = Mathf.Lerp(0, 1, t_forLerp) * timeUntilGameEndSec;
        fillImage.color = Color.Lerp(sunColor, moonColor, t_forLerp);
    }

    public void RepresentCurrentScore(float currentScore)
    {
        _currentscore = currentScore;
        currentStarSlider.value = currentScore;
    }

    private void ShowScoreAsStars()
    {
        endGameStarSlider.value = _currentscore;
    }
    IEnumerator WaitUntilTheEnd()
    {
        yield return waitUntilEnd;

        AudioManager.instance.StopAudioOver();
        ShowScoreAsStars();
        endGamePanel.SetActive(true);
        pauseMenu.SetActive(false);
        _dayTimeObject.SetActive(false );
        _inGameStarsObject.SetActive(false);
        Time.timeScale = 0f;
        gameEnded = true;
    }
}
