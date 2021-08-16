using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("Game References")]
    public static bool gamePaused;
    public GameObject pauseMenuUI;
    public bool levelComplete;

    public GameObject levelCompleteUI;

    // Start is called before the first frame update
    void Start()
    {
        levelComplete = false;
        gamePaused = false;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Play();
            }
            else
            {
                Pause();
            }
        }

        if (levelComplete)
        {
            levelCompleteUI.SetActive(true);
        }
    }

    public void Play()
    {
        pauseMenuUI.SetActive(false);
        gamePaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        gamePaused = true;
    }
    public void Quitting()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
