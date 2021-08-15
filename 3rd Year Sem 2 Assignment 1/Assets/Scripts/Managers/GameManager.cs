using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("Game References")]
    public static bool gamePaused;
    public GameObject pauseMenuUI; 

    // Start is called before the first frame update
    void Start()
    {
        
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
}
