using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlockController : MonoBehaviour
{

    public Transform parentTrans;

    public PlayerObserver playerObserver;

    // Start is called before the first frame update
    void Start()
    {
        playerObserver = GameObject.Find("Player Observer").GetComponent<PlayerObserver>();
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
            parentTrans.parent = collision.transform;
        }
    }
}
