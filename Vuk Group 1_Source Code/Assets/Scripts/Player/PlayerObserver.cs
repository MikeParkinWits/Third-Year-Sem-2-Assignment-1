using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObserver : MonoBehaviour
{
    public Transform playerTransform;

    public Transform playerMoveTo;

    public bool canMove;

    public GoalManager goalManager;

    public bool spikeCanMove;

    public static bool wrongShapeAudioPlayed;


    private void Start()
    {

        wrongShapeAudioPlayed = false;

        spikeCanMove = true;

        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        playerMoveTo = GameObject.Find("Player Move To").GetComponent<Transform>();

        goalManager = GameObject.Find("Goal").GetComponent<GoalManager>();
    }

    private void FixedUpdate()
    {

        if(playerTransform != null && playerMoveTo != null)
        {
            if (Vector3.Distance(playerTransform.position, playerMoveTo.position) == 0f
            && !GameManager.gamePaused)
            {
                Invoke("CanMoveTrue", 0.05f);
            }
            else
            {
                canMove = false;
                goalManager.goalChecked = false;
            }
        }


    }

    public void CanMoveTrue()
    {
        canMove = true;
    }


}
