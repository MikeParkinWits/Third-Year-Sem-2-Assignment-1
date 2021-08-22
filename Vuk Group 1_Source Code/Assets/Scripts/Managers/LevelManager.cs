using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public Transform levelButtons;

    public List<Transform> lockedLevelButtons;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform trans in levelButtons)

        {
            lockedLevelButtons.Add(trans);
        }

        for (int i = 0; i < lockedLevelButtons.Count; i++)
        {
            if (PlayerPrefs.GetInt("Level " + (i + 1) + " Complete") == 1)
            {
                if (i + 1 < lockedLevelButtons.Count)
                {
                    lockedLevelButtons[i + 1].gameObject.SetActive(false);
                }
            }
        }
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    
}
