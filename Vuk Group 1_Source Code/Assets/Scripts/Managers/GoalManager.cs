using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour
{

    public List<GoalTriggers> goalTrigger = new List<GoalTriggers>();

    public PlayerObserver playerObserver;

    public PlayerController playerController;

    public bool playerInGoal;

    public bool goalChecked;

    private GameManager gameManager;

    private bool playerInGoalCheck;

    private bool winAudioPlayed = false;

    private void Awake()
    {

        goalChecked = false;

        playerObserver = GameObject.Find("Player Observer").GetComponent<PlayerObserver>();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            goalTrigger.Add(gameObject.transform.GetChild(i).GetComponent<GoalTriggers>());
        }

        playerInGoal = true;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObserver.canMove && goalChecked == false)
        {
            GoalChecker();
        }

        if (!playerObserver.canMove)
        {
            Invoke("CannotMove", 0.05f);
        }
    }


    public void CannotMove()
    {
        PlayerObserver.wrongShapeAudioPlayed = false;
        playerInGoal = true;
    }

    public void GoalChecker()
    {

        for (int i = 0; i < goalTrigger.Count; i++)
        {

            if (goalTrigger[i].inGoal == true && !playerInGoalCheck)
            {
                playerInGoalCheck = true;

                Debug.Log(playerInGoalCheck);
            }
        }

        if (playerInGoalCheck)
        {



            for (int i = 0; i < goalTrigger.Count; i++)
            {
                Debug.Log(i + " IN GOALED" + goalTrigger[i].inGoal);


                if (goalTrigger[i].inGoal == false && playerInGoal)
                {
                    playerInGoal = false;

                    Debug.Log(i + " IN GOAL" + playerInGoal);

                    if (!PlayerObserver.wrongShapeAudioPlayed)
                    {
                        PlayerObserver.wrongShapeAudioPlayed = true;

                    }
                }


                if (i == goalTrigger.Count - 1)
                {
                    goalChecked = true;


                }

            }


            if (playerInGoalCheck && goalChecked)
            {
                if (playerInGoal)
                {


                    playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

                    if (playerController.movingBlockArray.Count + 1 == goalTrigger.Count)
                    {
                        if (!gameManager.levelComplete)
                        {
                            AudioManager.winAudio.Play();
                        }

                        gameManager.levelComplete = true;

                        Time.timeScale = 0;

                        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + " Complete", 1);
                    }
                }

                playerInGoal = true;
                playerInGoalCheck = false;
                goalChecked = false;
            }
            else
            {

            }

            if (goalChecked && playerInGoalCheck && !playerInGoal)
            {

                playerInGoalCheck = false;
            }
        }

    }
}
