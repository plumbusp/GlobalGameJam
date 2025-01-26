using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
    [SerializeField] Slider endGameStarSlider;
    [SerializeField] Slider currentStarSlider;
    [SerializeField] GameObject endGamePanel;
    private float _currentscore;

    private void Start()
    {
        Time.timeScale = 1.0f;
        currentStarSlider.value = 0;
        endGamePanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            endGamePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void RepresentCurrentScore(float currentScore)
    {
        _currentscore = currentScore;
        currentStarSlider.value = currentScore;
    }

    public void ShowScoreAsStars()
    {
        endGameStarSlider.value = _currentscore;
    }
}
