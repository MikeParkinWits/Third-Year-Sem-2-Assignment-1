using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObserver : MonoBehaviour
{
    public Transform playerTransform;
    //public Transform leftPlayer;

    public Transform playerMoveTo;
    //public Transform leftPlayerMoveTo;

    public bool canMove;

    //public StarManager starManager;

    public GoalManager goalManager;

    public bool spikeCanMove;

    public static bool wrongShapeAudioPlayed;


    private void Start()
    {

        wrongShapeAudioPlayed = false;

        spikeCanMove = true;

        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        //leftPlayer = GameObject.Find("Player Left").GetComponent<Transform>();

        playerMoveTo = GameObject.Find("Player Move To").GetComponent<Transform>();
        //leftPlayerMoveTo = GameObject.Find("PlayerMoveTo").GetComponent<Transform>();

        //starManager = GameObject.Find("Game Manager").GetComponent<StarManager>();

        goalManager = GameObject.Find("Goal").GetComponent<GoalManager>();
    }

    private void FixedUpdate()
    {

        //Debug.Log(wrongShapeAudioPlayed);

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

        /*
        if (RightPlayerController.rightPlayAudio || LeftPlayerController.leftPlayAudio)
        {
            starManager.movementScore++;
            AudioStayManager.playerMoveAudio.Play();

            RightPlayerController.rightPlayAudio = false;
            LeftPlayerController.leftPlayAudio = false;
        }
        */


    }

    public void CanMoveTrue()
    {
        canMove = true;
    }


}
