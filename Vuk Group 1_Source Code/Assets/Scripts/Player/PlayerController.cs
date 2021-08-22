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

    public LayerMask movingBlockMoveTo;

    public static bool rightMove = true;

    public PlayerObserver playerObserver;

    [Header("Reference Variables")]
    private GameManager gameManager;

    [Header("Moving Block Variables")]
    public LayerMask movingBlock;

    [Header("Goal Variable")]
    private bool rightGoalReached;

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

        canMoveGlobalCount = 0;
        canMoveGlobalTotal = 0;

        canMoveGlobal = true;

        rightPlayAudio = false;

        rightMove = true;

        moveToPoint.parent = null;

        playerObserver = GameObject.Find("Player Observer").GetComponent<PlayerObserver>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        playerSpriteRenderer.color = new Color(0.4588f, 0.9101f, 1f, 1f);

        pergatory = GameObject.Find("Pergatory");
        pergatory.transform.parent = null;

    }

    // Update is called once per frame
    void Update()
    {
        playerTrans.position = Vector3.MoveTowards(transform.position, moveToPoint.position, moveSpeed * Time.deltaTime);

        if (playerObserver.canMove)
        {
            Movement();

            //Rotation();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Rotation()
    {

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

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)
                 || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            canMoveGlobal = true;

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {

                for (int i = 0; i < movingBlockArray.Count + 1; i++)
                {

                        if (i != 0)
                        {
 

                            if (movingBlockArray[i - 1].canMove == false)
                            {
                                canMoveGlobal = false;
                            }

                        
                    }

                }


                if (canMoveGlobal)
                {
                    canMoveGlobalCount = 0;
                    MovementHorizontal(-moveAmount);
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {

                for (int i = 0; i < movingBlockArray.Count + 1; i++)
                {
                    if (i != 0)
                    {
                        if (movingBlockArray[i - 1].canMove == false)
                        {
                            canMoveGlobal = false;
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
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)
                     || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {


            canMoveGlobal = true;

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {

                for (int i = 0; i < movingBlockArray.Count + 1; i++)
                {
                    if (i != 0)
                    {
                        if (movingBlockArray[i - 1].canMove == false)
                        {
                            canMoveGlobal = false;
                        }
                    }
                }

                if (canMoveGlobal)
                {
                    canMoveGlobalCount = 0;
                    MovementVertical(moveAmount);
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {

                for (int i = 0; i < movingBlockArray.Count + 1; i++)
                {
                    if (i != 0)
                    {

                        if (movingBlockArray[i - 1].canMove == false)
                        {
                            canMoveGlobal = false;
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
        yield return new WaitUntil(() => movingBlockArray[i - 1].checkDone == true);
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
                destroyCount++;
            }
        }

        if (destroyCount == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (firstChildOne[0] != null)
        {
            firstChildOne[0].gameObject.GetComponent<PlayerController>().enabled = true;

            playerObserver.playerTransform = firstChildOne[0];

            playerObserver.playerMoveTo = firstChildOne[0].gameObject.GetComponent<MovingBlockManager>().moveToPoint;

            firstChildOne[0].gameObject.GetComponent<MovingBlockManager>().moveToPoint.gameObject.GetComponent<FollowScript>().enabled = false;

            firstChildOne[0].parent = null;

            firstChildOne[0].gameObject.name = "Player";
            firstChildOne[0].gameObject.tag = "Player";

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


        Destroy(moveToPoint.gameObject);

        Destroy(this.gameObject);
    }

    public void Death()
    {

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

                AudioManager.playerMovementAudio.Play();
            }
        }
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

                AudioManager.playerMovementAudio.Play();

            }
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Right Goal")
        {
            rightGoalReached = true;
        }
    }

    public bool GetRightGoalReached()
    {
        return rightGoalReached;
    }

}
