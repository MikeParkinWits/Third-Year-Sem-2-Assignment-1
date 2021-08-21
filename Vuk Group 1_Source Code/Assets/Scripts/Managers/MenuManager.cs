using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ClearSave()
    {
        PlayerPrefs.SetInt("Level 2 Tutorial Complete", 0);

        for (int i = 1; i <= 10; i++)
        {
            PlayerPrefs.SetInt("Level " + i + " Complete", 0);
        }
    }
}
