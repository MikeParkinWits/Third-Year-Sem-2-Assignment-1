using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{

    public MovingBlockManager currentMovingBlockManager;

    public PlayerObserver playerObserver;

    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        playerObserver = GameObject.Find("Player Observer").GetComponent<PlayerObserver>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //Debug.Log("COLLIDE");


        if (playerObserver.canMove)
        {
            if (collision.tag == "Player")
            {

                playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

                playerController.DestroyBlock();

            }

            if (collision.tag == "MovingBlockCanConnect")
            {

                currentMovingBlockManager = collision.gameObject.GetComponent<MovingBlockManager>();

                Debug.Log(currentMovingBlockManager);

                currentMovingBlockManager.DestroyBlock();
            }


        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


    }
}
