using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float rnd;
    void Start()
    {
        Time.timeScale = 1.0f;
        AudioManager.instance.PlayAudio(MusicType.CosmicMenuMusic);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rnd = Random.Range(1, 4);
            switch (rnd)
            {
                case 1:
                    AudioManager.instance.PlayAudio(SFXType.BuublePop1);
                    break;
                case 2:
                    AudioManager.instance.PlayAudio(SFXType.BuublePop2);
                    break;
                default:
                    break;
            }
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
