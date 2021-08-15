using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlockController : MonoBehaviour
{

    public Transform parentTrans;

    public PlayerObserver playerObserver;
    public PlayerController playerController;

    public MovingBlockManager movingBlockManager;

    public SpriteRenderer playerSpriteRenderer;

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
            if (collision.tag == "Player" || collision.tag == "MovingBlockCanConnect")
            {
                //parentTrans.parent = GameObject.FindGameObjectWithTag("Player").transform;
                parentTrans.parent = collision.transform;
                playerController.canMoveGlobalTotal++;

                parentTrans.tag = "MovingBlockCanConnect";

                movingBlockManager.attached = true;

                Debug.Log(movingBlockManager.attached);
                playerController.movingBlockArray.Add(movingBlockManager);

                playerSpriteRenderer.color = new Color(0.4588f, 0.9101f, 1f, 1f);

                this.transform.parent.gameObject.SetActive(false);
            }

        }
    }
}
