using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    /* Uses raycasts to click on Interactable game objects in the scene - Can be used for enemies and clues too
    *  If a game object with the tag "Item" is hit:
    *  - The interactable script on the item will add it to the player inventory
    *  - Information about the item will appear
    */

    
    Camera cam;
    Inventory playerInventory;
    List<string> ClickableTags = new List<string>();
    GameObject gameManagerObj;
    public ClueInfo clueInfo;

    [SerializeField]
    private Transform attackArea; 


    void Start()
    {
        cam = Camera.main;
        playerInventory = gameObject.GetComponent<Inventory>();
        gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");

        ClickableTags.Add("Item");
        ClickableTags.Add("Door");
        ClickableTags.Add("Clue");
        //Add enemy here??

    }
    // function to determine attacking while we don't have the sprites for it. 


    private void PlayerAttack() { 
        //HWe create a box the size of attack area and keep everything inside a collider array
        // the overlap box works for transforms 
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackArea.position, attackArea.localScale, 0 ); 
        //then we check for each element and see if we have an enemy there

     //   foreach (Collider2D collider in colliders) {
      //      if( GameObject. == EnemyHealthAndAttack)
      //  }


    }

    void Update()
    {
        if (!clueInfo.infoIsVisible && Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition)); //Returns the gameobject collider the mouse clicks

            if (hit.collider != null && ClickableTags.Contains(hit.collider.gameObject.tag)) //Checks if info is visible and a collider was hit and if that collider has a gameobject tag from the list
            {
                float distance = Vector2.Distance(gameObject.transform.position, hit.transform.position); //Returns the distance between the player and the gameobject collider
                Debug.Log("you hit something");

                if(hit.collider.gameObject.tag == "Item") //Checks if its tag is "item"
                {
                    ClickableItem(hit, distance);
                }

                if((hit.collider.gameObject.tag == "Door")) //Checks if its tag is "door"
                {
                    ClickableDoor(hit, distance);
                }
               
                if (hit.collider.gameObject.tag == "Clue") //Checks if its tag is "clue"
                {
                   
                    ClickableClue(hit, distance); //Shows the player information - use item option if the clue will go in the inventory
                }
            }
        }

        else if (clueInfo.infoIsVisible && Input.GetMouseButtonDown(0))
        {
            Debug.Log("close popup");
            //If clue text is open and left mouse is clicked = Close Clue
            clueInfo.ClosePopUp();
        }
    }

    private void ClickableItem(RaycastHit2D hit, float distance)
    {
        Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>(); //Returns the interactable component on the gameobject hit

        if (distance <= interactable.radius) //Checks if its within range
        {
            interactable.Pickup(); //puts in inventory

            clueInfo.CluePopUp(interactable.cluePopUp,  interactable.itemInfo); //Shows the items information/clue
            clueInfo.infoIsVisible = true;
        }
    }

    private void ClickableDoor(RaycastHit2D hit, float distance)
    {
        Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>(); //Returns the interactable component on the gameobject hit

        if (distance <= interactable.radius) //Checks if its within range
        {
            //Checks if the player has a key in their inventory
            if (playerInventory.inventoryList.Contains(interactable.item))
            {
                Debug.Log("Has a key");
                gameManagerObj.GetComponent<Menus>().WinGame();
            }

            else
            {
                clueInfo.CluePopUp(interactable.cluePopUp, interactable.itemInfo);
            }
        }
    }

    private void ClickableClue(RaycastHit2D hit, float distance)
    {
        Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>(); //Returns the interactable component on the gameobject hit

        if (distance <= interactable.radius) //Checks if its within range
        {
            Debug.Log("Found a clue");
        }
    }
}
