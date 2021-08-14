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

    private void Awake()
    {

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
        //Debug.Log(playerController.canMoveGlobalCount);
    }

    public void MovementChecker()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                playerController.canMoveGlobalCount++;
                Debug.Log("Hello");
                MovementHorizontal(-playerController.moveAmount);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("Hello");
                playerController.canMoveGlobalCount++;
                MovementHorizontal(playerController.moveAmount);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("Hello");
                playerController.canMoveGlobalCount++;
                MovementVertical(playerController.moveAmount);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Debug.Log("Hello");
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

                Debug.Log("CAN MOVE");

                //playerController.canMoveGlobal = true;
                canMove = true;
                //rightPlayAudio = true;
            }
        }
        else
        {
            Debug.Log("CANNOT MOVE");

            //playerController.canMoveGlobal = false;
            canMove = false;
        }

        //SlidingBlocksHorizontal(moveAmount, moveAmount);

        //SwapTiles();

        //MovingBlock(true, moveAmount);
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

                Debug.Log("CAN MOVE");

                //rightPlayAudio = true;

                //playerController.canMoveGlobal = true;
                canMove = true;
            }
        }
        else
        {
            Debug.Log("CANNOT MOVE");

            //playerController.canMoveGlobal = false;

            canMove = false;
        }

        //SlidingBlocksVertical(moveAmount, moveAmount);

        //SwapTiles();

        //MovingBlock(false, moveAmount);
    }

    public void DestroyBlock()
    {
        //Transform firstChild = GameObject.Find("MovingBlock").transform.GetChild(0);

        Transform firstChild = this.gameObject.transform.Find("Moving Block");

        if (firstChild != null)
        {
            firstChild.parent = GameObject.FindGameObjectWithTag("Player").transform;

            playerController.movingBlockArray.Remove(this.gameObject.GetComponent<MovingBlockManager>());
        }

        Destroy(moveToPoint.gameObject);

        Destroy(this.gameObject);

    }

}
