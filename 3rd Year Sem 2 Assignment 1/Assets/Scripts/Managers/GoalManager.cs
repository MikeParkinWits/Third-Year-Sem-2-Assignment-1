using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{

    public List<GoalTriggers> goalTrigger = new List<GoalTriggers>();

    public PlayerObserver playerObserver;

    public PlayerController playerController;

    public bool playerInGoal;

    public bool goalChecked;

    private void Awake()
    {

        goalChecked = false;

        playerObserver = GameObject.Find("Player Observer").GetComponent<PlayerObserver>();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            goalTrigger.Add(gameObject.transform.GetChild(i).GetComponent<GoalTriggers>());
        }

        playerInGoal = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObserver.canMove && goalChecked == false)
        {
            GoalChecker();
        }
    }

    public void GoalChecker()
    {
        goalChecked = false;
        playerInGoal = true;

            for (int i = 0; i < goalTrigger.Count; i++)
            {

                    //Debug.Log(i + "" + goalTrigger[i].inGoal);

                //Debug.Log("GOAL: " + goalTrigger[i].inGoal);

                if (goalTrigger[i].inGoal == false)
                    {
                        playerInGoal = false;
                        //Debug.Log("FALSE GOAL " + goalTrigger[i].inGoal);
                    }

                    if (i == goalTrigger.Count - 1)
                    {
                        goalChecked = true;
                    }
                
            }

        

        if (playerInGoal && goalChecked)
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            if (playerController.movingBlockArray.Count + 1 == goalTrigger.Count)
            {
                Debug.Log("WIN!");

                Time.timeScale = 0;
            }
        }
        else
        {
            //goalChecked = false;
        }

    }
}
