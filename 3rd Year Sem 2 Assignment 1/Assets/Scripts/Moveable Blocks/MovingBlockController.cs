using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlockController : MonoBehaviour
{

    public Transform parentTrans;

    public PlayerObserver playerObserver;
    public PlayerController playerController;

    public MovingBlockManager movingBlockManager;

    // Start is called before the first frame update
    void Start()
    {
        playerObserver = GameObject.Find("Player Observer").GetComponent<PlayerObserver>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("INSIDE");
        if (playerObserver.canMove)
        {
            if (collision.tag == "Player" || collision.tag == "MovingBlock")
            {
                parentTrans.parent = GameObject.FindGameObjectWithTag("Player").transform;
                //parentTrans.parent = collision.transform;
                playerController.canMoveGlobalTotal++;

                movingBlockManager.attached = true;
                playerController.movingBlockArray.Add(movingBlockManager);

                this.gameObject.SetActive(false);
            }

        }
    }
}
