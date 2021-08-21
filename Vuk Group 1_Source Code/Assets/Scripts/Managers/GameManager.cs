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

    public MovingBlockManager movingBlockManager;

    private float timer = 0;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        levelComplete = false;
        gamePaused = false;
        Time.timeScale = 1f;

        timer = 0f;
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

        if (SceneManager.GetActiveScene().name == "Level 2")
        {

            Debug.Log(movingBlockManager.attached);

            Debug.Log(PlayerPrefs.GetInt("Level 2 Tutorial Complete"));

            if (!movingBlockManager.attached && PlayerPrefs.GetInt("Level 2 Tutorial Complete") != 1)
            {
                if (timer <= 14)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    anim.Play("Level Two Animation");
                }

                Debug.Log(timer);
            }
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
        //debug.Log("Quitting Game");
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
