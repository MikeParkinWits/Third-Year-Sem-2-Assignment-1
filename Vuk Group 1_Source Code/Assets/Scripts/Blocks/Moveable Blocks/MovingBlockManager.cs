using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlockManager : MonoBehaviour
{

    public Transform moveableBlockCheckTransform;
    public Transform moveToPoint;

    public PlayerController playerController;

    public bool attached;

    public bool canMove;

    public bool checkDone;

    private void Awake()
    {

        canMove = true;

        checkDone = false;

        moveableBlockCheckTransform = GameObject.Find("MoveableBlockChecker").GetComponent<Transform>();

        attached = false;

        moveableBlockCheckTransform.parent = null;
        moveToPoint.parent = moveableBlockCheckTransform;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attached)
        {
            MovementChecker();
        }
        ////debug.Log(playerController.canMoveGlobalCount);
    }

    public void MovementChecker()
    {
        canMove = true;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)
                || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                playerController.canMoveGlobalCount++;
                ////debug.Log("Hello");
                MovementHorizontal(-playerController.moveAmount);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                ////debug.Log("Hello");
                playerController.canMoveGlobalCount++;
                MovementHorizontal(playerController.moveAmount);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)
                     || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                ////debug.Log("Hello");
                playerController.canMoveGlobalCount++;
                MovementVertical(playerController.moveAmount);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                ////debug.Log("Hello");
                playerController.canMoveGlobalCount++;
                MovementVertical(-playerController.moveAmount);
            }
        }
    }

    public void MovementHorizontal(float moveAmount)
    {
        if (!Physics2D.OverlapPoint(moveToPoint.position + new Vector3(moveAmount, 0f, 0f), playerController.whatStopsMovement1))
        {
            if (!(Physics2D.OverlapPoint(transform.position + new Vector3(moveAmount * 2, 0f, 0f), playerController.whatStopsMovement1)
                    && (Physics2D.OverlapPoint(transform.position + new Vector3(moveAmount, 0f, 0f), playerController.movingBlock)
                        || Physics2D.OverlapPoint(transform.position + new Vector3(moveAmount * 2, 0f, 0f), playerController.goalMask))))
            {
                //moveToPoint.position += new Vector3(moveAmount, 0f, 0f);

                //debug.Log("CAN MOVE");

                //playerController.canMoveGlobal = true;
                canMove = true;
                //rightPlayAudio = true;
            }
        }
        else
        {
            canMove = false;

            //debug.Log("CANNOT MOVE");

            //playerController.canMoveGlobal = false;
        }

        //SlidingBlocksHorizontal(moveAmount, moveAmount);

        //SwapTiles();

        //MovingBlock(true, moveAmount);

        checkDone = true;
    }

    public void MovementVertical(float moveAmount)
    {
        if (!Physics2D.OverlapPoint(moveToPoint.position + new Vector3(0f, moveAmount, 0f), playerController.whatStopsMovement1))
        {
            if (!(Physics2D.OverlapPoint(transform.position + new Vector3(0f, moveAmount, 0f), playerController.movingBlock)
                    && (Physics2D.OverlapPoint(transform.position + new Vector3(0f, moveAmount * 2, 0f), playerController.whatStopsMovement1)
                        || Physics2D.OverlapPoint(transform.position + new Vector3(0f, moveAmount * 2, 0f), playerController.goalMask))))
            {
                //moveToPoint.position += new Vector3(0f, moveAmount, 0f);

                //debug.Log("CAN MOVE");

                //rightPlayAudio = true;

                //playerController.canMoveGlobal = true;
                canMove = true;
            }
        }
        else
        {
            //debug.Log("CANNOT MOVE");

            //playerController.canMoveGlobal = false;

            canMove = false;
        }

        //SlidingBlocksVertical(moveAmount, moveAmount);

        //SwapTiles();

        //MovingBlock(false, moveAmount);

        checkDone = true;
    }

    public void DestroyBlock()
    {
        //Transform firstChild = GameObject.Find("MovingBlock").transform.GetChild(0);

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        Transform firstChild = this.gameObject.transform.Find("Moving Block");

        if (firstChild != null)
        {
            firstChild.parent = GameObject.FindGameObjectWithTag("Player").transform;

        }
        playerController.movingBlockArray.Remove(this.gameObject.GetComponent<MovingBlockManager>());
        

        Destroy(moveToPoint.gameObject);

        Destroy(gameObject);

    }

}
