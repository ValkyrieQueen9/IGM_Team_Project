using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInteract : ClueInfo
{
    /* Uses raycasts to click on Interactable game objects in the scene - Can be used for enemies and clues too
    *  If a game object with the tag "Item" is hit:
    *  - The interactable script on the item will add it to the player inventory
    *  - Information about the item will appear
    */

    Camera cam;
    Inventory playerInventory;
    public Menus menu;
    [SerializeField]
    private Transform attackArea; 


    void Start()
    {
        cam = Camera.main;
        playerInventory = gameObject.GetComponent<Inventory>();
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
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition)); //Returns the gameobject collider the mouse clicks

            if (!infoIsVisible && hit.collider != null) //Checks if info is visible and a collider was hit 
            {
                float distance = Vector2.Distance(gameObject.transform.position, hit.transform.position); //Returns the distance between the player and the gameobject collider
                Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>(); //Returns the interactable component on the gameobject hit

                if(distance <= interactable.radius) //checks if the player is in range of the gameobject collider
                {
                    Debug.Log("you hit something");

                    if (hit.collider.gameObject.tag == "Item") //Checks if its tag is "Item"
                    {
                        {
                            interactable.Pickup(); //puts in inventory

                            CluePopUp(interactable.cluePopUp,  interactable.itemInfo); //Shows the items information/clue
                            infoIsVisible = true;
                        }
                    }

                    if (hit.collider.gameObject.tag == "Door") //Checks if its tag is "door"
                    {
                        //Checks if the player has a key in their inventory
                        if (playerInventory.inventoryList.Contains(interactable.item))
                        {
                            Debug.Log("Has a key");
                            menu.WinGame();
                        }
                        else
                        {
                            CluePopUp(interactable.cluePopUp, interactable.itemInfo);
                        }
                    }


                    if (hit.collider.gameObject.tag == "Clue") //Checks if its tag is "clue"
                    {
                        //Shows the player information - use item option if the clue will go in the inventory
                    }
                }
            }
            else if (infoIsVisible && Input.GetMouseButtonDown(0))
            {
                //If clue text is open and left mouse is clicked = Close Clue
                ClosePopUp();
            }
        }
    }
}
