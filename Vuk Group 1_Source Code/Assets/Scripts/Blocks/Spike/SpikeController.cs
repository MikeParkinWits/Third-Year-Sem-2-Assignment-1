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
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        playerObserver = GameObject.Find("Player Observer").GetComponent<PlayerObserver>();
        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        ////debug.Log("COLLIDE");

        playerObserver.spikeCanMove = false;

            if (collision.tag == "Player")
            {

                playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            playerController.playerSpriteRenderer.enabled = false;


                Invoke("DestroyPlayer", 0.5f);

            }

            if (collision.tag == "MovingBlockCanConnect")
            {

                currentMovingBlockManager = collision.gameObject.GetComponent<MovingBlockManager>();

                //debug.Log(currentMovingBlockManager);

                currentMovingBlockManager.DestroyBlock();

            playerObserver.spikeCanMove = true;
            }


        

    }

    public void DestroyPlayer()
    {
        if (playerObserver.canMove)
        {
            playerController.DestroyBlock();
        }
        else
        {
            StartCoroutine(DestroyTester());
        }

        //Destroy(playerController.gameObject);

        playerObserver.spikeCanMove = true;
    }

    public IEnumerator DestroyTester()
    {
        yield return new WaitUntil(() => playerObserver.canMove);

        playerController.DestroyBlock();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


    }
}
