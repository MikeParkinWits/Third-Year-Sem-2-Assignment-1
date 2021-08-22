using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerObserver.canMove)
        {
            if (collision.tag == "Player" || collision.tag == "MovingBlockCanConnect")
            {
                playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

                AudioManager.clickAudio.Play();

                parentTrans.parent = collision.transform;
                playerController.canMoveGlobalTotal++;

                parentTrans.tag = "MovingBlockCanConnect";


                movingBlockManager.attached = true;

                if (SceneManager.GetActiveScene().name == "Level 2")
                {
                    PlayerPrefs.SetInt("Level 2 Tutorial Complete", 1);
                }

                if (!playerController.movingBlockArray.Contains(movingBlockManager))
                {
                    playerController.movingBlockArray.Add(movingBlockManager);
                    playerController.movingBlockTransformList.Add(movingBlockManager.transform);
                }

                playerSpriteRenderer.color = new Color(0.4588f, 0.9101f, 1f, 1f);

                this.transform.parent.gameObject.SetActive(false);
            }

        }
    }
}
