using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorPortal : MonoBehaviour
{

    public string doorName;
    public GameObject destination;
    public bool isTeleporting;
    public float doorCooldownTime;
    public bool canTeleport;
    public GameObject[] doorPortals;

    private GameObject player;

    /*
     * When player hits collider on gameobject, send the player to the destination room
     * 
     */

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isTeleporting = false;
        doorPortals = GameObject.FindGameObjectsWithTag("Door");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if(!isTeleporting && )
        {
            Debug.Log("Has Collided");
            collision.transform.position = destination.transform.position;
            isTeleporting = true;
        }
        */
        StartCoroutine(DoorCooldown(doorCooldownTime, canTeleport));

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(isTeleporting && collision.gameObject.tag == "Player")
        {
            Debug.Log("stopped colliding");
            collision.transform.position = destination.transform.position;
            isTeleporting = false;
        }
    }

    IEnumerator DoorCooldown(float doorCooldown, bool canTeleport)
    {
        Debug.Log("cooldown started");
        canTeleport = false;
        yield return new WaitForSeconds(doorCooldown);
        canTeleport = true;
        Debug.Log("cooldown ended");

    }
}
