using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField]private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header ("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [Header("Finish")]
    [SerializeField] private GameObject finishScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        finishScreen.SetActive(false);
    }

    #region Game Over

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    #endregion Game Over

    #region Pause

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameOverScreen.activeInHierarchy || finishScreen.activeInHierarchy) return;
            if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }

        }
    }

    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);

        if(status)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion Pause

    #region Finish

    public void LevelComplete()
    {
        finishScreen.SetActive(true);
    }


    #endregion Finish
}
