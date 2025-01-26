using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] Collider2D blockCollider;
    private bool paused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        AudioManager.instance.StopAudioOver();
        AudioManager.instance.StopAudioOverTwo();
        pausePanel.SetActive(true);
        paused = true;
        blockCollider.enabled = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        paused = false;
        blockCollider.enabled = false;
        pausePanel.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        paused = false;
        blockCollider.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        paused = false;
        blockCollider.enabled = false;
        SceneManager.LoadScene(0);
    }
}
