using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public List<Transform> movingBlockTransformList = new List<Transform>();

    int rotateValue = 0;

    public Transform playerTrans;

    public SpriteRenderer playerSpriteRenderer;

    public bool canDestroy = false;

    public GameObject pergatory;

    public int destroyCount = 0;

    // Start is called before the first frame update
    void Start()
    {

        playerTrans = this.transform;

        //movingBlockArray.Clear();

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

        playerSpriteRenderer.color = new Color(0.4588f, 0.9101f, 1f, 1f);

        pergatory = GameObject.Find("Pergatory");
        pergatory.transform.parent = null;

    }

    // Update is called once per frame
    void Update()
    {
        playerTrans.position = Vector3.MoveTowards(transform.position, moveToPoint.position, moveSpeed * Time.deltaTime);

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

            //Rotation();
        }

        //Transform[] children = this.gameObject.transform.Find("Moving Block").GetComponentsInChildren<Transform>();
        ////debug.Log(movingBlockTransformList.Count);
    }

    public void Rotation()
    {

        //MUST CHECK FOR WALL COLLISION, SAME WAY AS MOVEMENT

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (rotateValue > 360)
            {
                rotateValue = 0;
            }

            rotateValue += 90;

            this.transform.eulerAngles = new Vector3(0, 0, rotateValue);
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
 
                            //debug.Log(i + "" + movingBlockArray[i - 1].canMove);

                            if (movingBlockArray[i - 1].canMove == false)
                            {
                                canMoveGlobal = false;
                                //debug.Log("FALSE");
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
                        //debug.Log(i + "" + movingBlockArray[i - 1].canMove + " " + movingBlockArray[i - 1].attached);

                        if (movingBlockArray[i - 1].canMove == false)
                        {
                            canMoveGlobal = false;
                            //debug.Log("FALSE");
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
                        //debug.Log(i + "" + movingBlockArray[i - 1].canMove + " " + movingBlockArray[i - 1].attached);

                        if (movingBlockArray[i - 1].canMove == false)
                        {
                            canMoveGlobal = false;
                            //debug.Log("FALSE");
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
                        //debug.Log(i + "" + movingBlockArray[i - 1].canMove);

                        if (movingBlockArray[i - 1].canMove == false)
                        {
                            canMoveGlobal = false;
                            //debug.Log("FALSE");
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

    public IEnumerator CheckMove(int i)
    {
        //debug.Log("Waiting for princess to be rescued...");
        yield return new WaitUntil(() => movingBlockArray[i - 1].checkDone == true);
        //debug.Log("Princess was rescued!");
    }

    public void DestroyBlock()
    {
        List<Transform> firstChildOne = new List<Transform>();

        Transform[] children = this.gameObject.transform.GetComponentsInChildren<Transform>();

        Debug.Log(children.Length);

        foreach (Transform trans in children)
        {
            if (trans.name == "Moving Block")
            {
                firstChildOne.Add(trans);
                trans.parent = pergatory.transform;
                ////debug.Log("child" + trans.name);
                destroyCount++;
            }
        }

        if (destroyCount == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //firstChildOne = this.gameObject.transform.Find("Moving Block");

        //Transform firstChild = this.gameObject.transform.Find("Moving Block");

        ////debug.Log("First Child" + firstChild);

        if (firstChildOne[0] != null)
        {
            firstChildOne[0].gameObject.GetComponent<PlayerController>().enabled = true;

            /*

            for (int i = 1; i < movingBlockArray.Count; i++)
            {
                firstChildOne[0].gameObject.GetComponent<PlayerController>().movingBlockArray.Add(movingBlockArray[i]);

                //debug.Log("Moving Block Array " + movingBlockArray[i]);

                //debug.Log("Moving Block Array " + movingBlockArray.Count);


            }

            */


            foreach (MovingBlockManager item in this.movingBlockArray)
            {
                //print("GO GO " + item.name);
            }

            playerObserver.playerTransform = firstChildOne[0];

            playerObserver.playerMoveTo = firstChildOne[0].gameObject.GetComponent<MovingBlockManager>().moveToPoint;

            firstChildOne[0].gameObject.GetComponent<MovingBlockManager>().moveToPoint.gameObject.GetComponent<FollowScript>().enabled = false;

            firstChildOne[0].parent = null;

            firstChildOne[0].gameObject.name = "Player";
            firstChildOne[0].gameObject.tag = "Player";

            //firstChild.gameObject.GetComponent<PlayerController>().movingBlockArray = this.gameObject.GetComponent<PlayerController>().movingBlockArray;

            //debug.Log("LOST");

            Transform[] childTest = pergatory.transform.GetComponentsInChildren<Transform>();

            foreach (Transform trans in childTest)
            {
                if (trans.name == "Moving Block")
                {
                    trans.parent = firstChildOne[0].transform;
                    ////debug.Log("child" + trans.name);
                    firstChildOne[0].gameObject.GetComponent<PlayerController>().movingBlockArray.Add(trans.GetComponent<MovingBlockManager>());
                }
            }

        }



        /*

        foreach (Transform child in movingBlockTransformList)
        {
            if (child != firstChildOne[0])
            {
                child.parent = firstChildOne[0].transform;
            }
        }

        */

        Destroy(moveToPoint.gameObject);

        Destroy(this.gameObject);

        //this.gameObject.GetComponent<PlayerController>().enabled = false;
    }

    public void Death()
    {

    }

    /*
    public void DestroyBlock()
    {
        StartCoroutine(CheckPlayerDestroy());
    }

    public IEnumerator CheckPlayerDestroy()
    {
        List<Transform> firstChildOne = new List<Transform>();

        //Transform[] children = this.gameObject.transform.Find("Moving Block").GetComponentsInChildren<Transform>();

        //firstChildOne = this.gameObject.transform.Find("Moving Block");

        Transform firstChild = this.gameObject.transform.Find("Moving Block");

        //debug.Log("First Child" + firstChild);

        if (firstChild != null)
        {
            firstChild.gameObject.GetComponent<PlayerController>().enabled = true;

            for (int i = 1; i < movingBlockArray.Count; i++)
            {
                firstChild.gameObject.GetComponent<PlayerController>().movingBlockArray.Add(movingBlockArray[i]);

                if (i == movingBlockArray.Count - 1)
                {
                    canDestroy = true;
                }
            }

            foreach (MovingBlockManager item in this.movingBlockArray)
            {
                print("GO GO " + item.name);
            }

            playerObserver.playerTransform = firstChild;

            playerObserver.playerMoveTo = firstChild.gameObject.GetComponent<MovingBlockManager>().moveToPoint;

            firstChild.gameObject.GetComponent<MovingBlockManager>().moveToPoint.gameObject.GetComponent<FollowScript>().enabled = false;

            firstChild.parent = null;

            firstChild.gameObject.name = "Player";
            firstChild.gameObject.tag = "Player";

            //firstChild.gameObject.GetComponent<PlayerController>().movingBlockArray = this.gameObject.GetComponent<PlayerController>().movingBlockArray;

            //debug.Log("LOST");

        }

        foreach (Transform child in movingBlockTransformList)
        {
            if (child != firstChild)
            {
                child.parent = firstChild.transform;
            }
        }


        if (firstChild == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        yield return new WaitUntil(() => canDestroy);

        Destroy(moveToPoint.gameObject);

        Destroy(this.gameObject);
        //this.gameObject.GetComponent<PlayerController>().enabled = false;
    }

    */

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
