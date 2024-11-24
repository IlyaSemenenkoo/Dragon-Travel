using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenuScreen;
    private LevelGenerator levelGenerator;

    private void Awake()
    {
        mainMenuScreen.SetActive(true);
        levelGenerator = new LevelGenerator();
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(1);
        //levelGenerator.StartGenerate();
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
