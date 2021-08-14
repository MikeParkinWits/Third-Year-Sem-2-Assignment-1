using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Variables")]
    public float moveSpeed = 4f;
    public float moveAmount = 1f;
    public Transform moveToPoint;
    public LayerMask whatStopsMovement1;
    public LayerMask goalMask;

    public static bool isSliding = false;
    public LayerMask slidingBlocks;
    public LayerMask movingBlockMoveTo;

    public static bool rightMove = true;

    public PlayerObserver playerObserver;

    [Header("Swap Variables")]
    public LayerMask swapMask;

    [Header("Reference Variables")]
    private GameManager gameManager;

    [Header("Moving Block Variables")]
    public LayerMask movingBlock;
    private float time = 0;

    [Header("Goal Variable")]
    private bool rightGoalReached;

    private bool fallAudioPlayed;

    //public StarManager starManager;

    public static bool rightPlayAudio;

    public bool canMoveGlobal;
    public int canMoveGlobalCount;
    public int canMoveGlobalTotal;

    public List<MovingBlockManager> movingBlockArray = new List<MovingBlockManager>();

    // Start is called before the first frame update
    void Start()
    {

        movingBlockArray.Clear();

        canMoveGlobalCount = 0;
        canMoveGlobalTotal = 0;

        canMoveGlobal = true;

        rightPlayAudio = false;

        rightMove = true;

        moveToPoint.parent = null;

        //MovingObject.moveBlockHorizontal = false;

        playerObserver = GameObject.Find("Player Observer").GetComponent<PlayerObserver>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        //starManager = GameObject.Find("Game Manager").GetComponent<StarManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveToPoint.position, moveSpeed * Time.deltaTime);

        if (Physics2D.OverlapPoint(transform.position, slidingBlocks))
        {
            rightMove = false;
        }
        else
        {
            rightMove = true;
        }

        if (playerObserver.canMove)
        {
            Movement();
        }
    }

    public void Movement()
    {


        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            canMoveGlobal = true;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

                for (int i = 0; i < movingBlockArray.Count + 1; i++)
                {
                    if (i != 0)
                    {
                        Debug.Log(i + "" + movingBlockArray[i - 1].canMove);

                        if (movingBlockArray[i - 1].canMove == false)
                        {
                            canMoveGlobal = false;
                            Debug.Log("FALSE");
                        }

                    }
                }


                if (canMoveGlobal)
                {
                    canMoveGlobalCount = 0;
                    MovementHorizontal(-moveAmount);
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {

                for (int i = 0; i < movingBlockArray.Count + 1; i++)
                {
                    if (i != 0)
                    {
                        Debug.Log(i + "" + movingBlockArray[i - 1].canMove);

                        if (movingBlockArray[i - 1].canMove == false)
                        {
                            canMoveGlobal = false;
                            Debug.Log("FALSE");
                        }
                    }
                }

                if (canMoveGlobal)
                {
                    canMoveGlobalCount = 0;
                    MovementHorizontal(moveAmount);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {

            canMoveGlobal = true;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                for (int i = 0; i < movingBlockArray.Count + 1; i++)
                {
                    if (i != 0)
                    {
                        Debug.Log(i + "" + movingBlockArray[i - 1].canMove);

                        if (movingBlockArray[i - 1].canMove == false)
                        {
                            canMoveGlobal = false;
                            Debug.Log("FALSE");
                        }
                    }
                }

                if (canMoveGlobal)
                {
                    canMoveGlobalCount = 0;
                    MovementVertical(moveAmount);
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

                for (int i = 0; i < movingBlockArray.Count + 1; i++)
                {
                    if (i != 0)
                    {
                        Debug.Log(i + "" + movingBlockArray[i - 1].canMove);

                        if (movingBlockArray[i - 1].canMove == false)
                        {
                            canMoveGlobal = false;
                            Debug.Log("FALSE");
                        }
                    }
                }

                if (canMoveGlobal)
                {
                    canMoveGlobalCount = 0;
                    MovementVertical(-moveAmount);
                }
            }
        }
    }

    public void MovementHorizontal(float moveAmount)
    {
        if (!Physics2D.OverlapPoint(moveToPoint.position + new Vector3(moveAmount, 0f, 0f), whatStopsMovement1))
        {
            if (!(Physics2D.OverlapPoint(transform.position + new Vector3(moveAmount * 2, 0f, 0f), whatStopsMovement1)
                    && (Physics2D.OverlapPoint(transform.position + new Vector3(moveAmount, 0f, 0f), movingBlock)
                        || Physics2D.OverlapPoint(transform.position + new Vector3(moveAmount * 2, 0f, 0f), goalMask))))
            {
                moveToPoint.position += new Vector3(moveAmount, 0f, 0f);

                //rightPlayAudio = true;
            }
        }

        //SlidingBlocksHorizontal(moveAmount, moveAmount);

        //SwapTiles();

        //MovingBlock(true, moveAmount);
    }

    public void MovementVertical(float moveAmount)
    {
        if (!Physics2D.OverlapPoint(moveToPoint.position + new Vector3(0f, moveAmount, 0f), whatStopsMovement1))
        {
            if (!(Physics2D.OverlapPoint(transform.position + new Vector3(0f, moveAmount, 0f), movingBlock)
                    && (Physics2D.OverlapPoint(transform.position + new Vector3(0f, moveAmount * 2, 0f), whatStopsMovement1)
                        || Physics2D.OverlapPoint(transform.position + new Vector3(0f, moveAmount * 2, 0f), goalMask))))
            {
                moveToPoint.position += new Vector3(0f, moveAmount, 0f);

                //rightPlayAudio = true;
            }
        }
        //SlidingBlocksVertical(moveAmount, moveAmount);

        //SwapTiles();

        //MovingBlock(false, moveAmount);
    }

    /*
     *     public void SlidingBlocksVertical(float moveAmount, float adjustmentValue)
    {
        if (Physics2D.OverlapPoint(transform.position + new Vector3(0f, moveAmount, 0f), slidingBlocks))
        {
            StartCoroutine(SlideMoveVertical(moveAmount));
        }
        else
        {
            time = 0f;
        }
    }

     */

    IEnumerator SlideMoveVertical(float moveAmount)
    {
        yield return new WaitForSeconds(0.1f);

        while (!Physics2D.OverlapPoint(moveToPoint.position + new Vector3(0f, moveAmount, 0f), whatStopsMovement1)
                && !Physics2D.OverlapPoint(moveToPoint.position + new Vector3(0f, moveAmount, 0f), movingBlockMoveTo)
                    && Physics2D.OverlapPoint(moveToPoint.position, slidingBlocks))
        {
            moveToPoint.position += new Vector3(0f, moveAmount, 0f);
        }
    }

    /*
     
    public void SlidingBlocksHorizontal(float moveAmount, float adjustmentValue)
    {
        if (Physics2D.OverlapPoint(transform.position + new Vector3(moveAmount, 0f, 0f), slidingBlocks))
        {
            StartCoroutine(SlideMoveHorizontal(moveAmount));
        }
    }

     */

    IEnumerator SlideMoveHorizontal(float moveAmount)
    {
        yield return new WaitForSeconds(0.1f);

        while (!Physics2D.OverlapPoint(moveToPoint.position + new Vector3(moveAmount, 0f, 0f), whatStopsMovement1)
                    && !Physics2D.OverlapPoint(moveToPoint.position + new Vector3(moveAmount, 0f, 0f), movingBlockMoveTo)
                        && Physics2D.OverlapPoint(moveToPoint.position, slidingBlocks))
        {
            moveToPoint.position += new Vector3(moveAmount, 0f, 0f);
        }
    }


    public void HoleTile(Collider2D collision)
    {
        if (collision.tag == "Hole")
        {
            //GameManager.gamePaused = true;
            transform.position = UnityEngine.Vector3.Lerp(transform.position, transform.position, Time.time);

            if (transform.localScale.x >= 0f)
            {

                /*
                if (!AudioStayManager.fallAudio.isPlaying && !fallAudioPlayed)
                {
                    fallAudioPlayed = true;

                    AudioStayManager.fallAudio.Play();
                }
                */

                transform.localScale += new UnityEngine.Vector3(0.1F, 0.1f, 0.1f) * -0.1f; //Making the piece disappear
            }
            else
            {
                //gameManager.ResetScene();
            }
        }
    }

    public void SwapTiles()
    {
        if (!Physics2D.OverlapCircle(moveToPoint.position, 0.2f, swapMask))
        {
            //SwapTile.canSwapRight = true;
        }
    }

    /*
         public void MovingBlock(bool horizontal, float moveAmount)
    {
        if (horizontal)
        {
            if (Physics2D.OverlapPoint(transform.position + new Vector3(moveAmount, 0f, 0f), movingBlock)
                    && !Physics2D.OverlapPoint(transform.position + new Vector3(moveAmount * 2, 0f, 0f), whatStopsMovement1)
                        && !Physics2D.OverlapPoint(transform.position + new Vector3(moveAmount * 2, 0f, 0f), goalMask))
            {
                MovingObject.blockPushed = true;

                MovingObject.moveBlockHorizontal = horizontal;
                MovingObject.pushDirection = moveAmount;
            }
        }
        else
        {
            if (Physics2D.OverlapPoint(transform.position + new Vector3(0f, moveAmount, 0f), movingBlock)
                    && !Physics2D.OverlapPoint(transform.position + new Vector3(0f, moveAmount * 2, 0f), whatStopsMovement1)
                        && !Physics2D.OverlapPoint(transform.position + new Vector3(0f, moveAmount * 2, 0f), goalMask))
            {
                MovingObject.blockPushed = true;

                MovingObject.moveBlockHorizontal = horizontal;
                MovingObject.pushDirection = moveAmount;
            }
        }

    }

     */



    private void OnTriggerStay2D(Collider2D collision)
    {
        HoleTile(collision);

        if (collision.tag == "Right Goal")
        {
            rightGoalReached = true;
            //GameManager.goalReached = true;
        }
    }

    public bool GetRightGoalReached()
    {
        return rightGoalReached;
    }

}
