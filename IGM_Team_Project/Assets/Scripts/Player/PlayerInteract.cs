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

    public ClueManager clueManager;

    List<string> ClickableTags = new List<string>();

    [SerializeField]
    private Transform attackArea; 


    void Start()
    {
        ClickableTags.Add("Item");
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
        if (!clueManager.infoIsVisible && Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition)); //Returns the gameobject collider the mouse clicks

            if (hit.collider != null && ClickableTags.Contains(hit.collider.gameObject.tag)) //Checks if a collider was hit and if that collider has a gameobject tag from the list
            {
                float distance = Vector2.Distance(gameObject.transform.position, hit.transform.position); //Returns the distance between the player and the gameobject collider

                if(hit.collider.gameObject.tag == "Item") //Checks if its tag is "item"
                {
                    ClickableItem(hit, distance);
                }
               
                if (hit.collider.gameObject.tag == "Clue") //Checks if its tag is "clue"
                {
                    ClickableClue(hit, distance); //Shows the player information - use item option if the clue will go in the inventory
                }
            }
        }

        else if (clueManager.infoIsVisible && Input.GetMouseButtonDown(0))
        {
            //If clue text is open and left mouse is clicked = Close Clue
            clueManager.ClosePopUp();
        }
    }

    private void ClickableItem(RaycastHit2D hit, float distance) // Something the player can put in their inventory and will need to unlock a clue (key, ink etc.)
    {
        Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>(); //Returns the interactable component on the gameobject hit

        if (distance <= interactable.radius) //Checks if its within range
        {
            interactable.Pickup(); //puts in inventory
            clueManager.CluePopUp(interactable.cluePopUp,  interactable.itemInfo); //Shows the items information/clue
        }
    }

    private void ClickableClue(RaycastHit2D hit, float distance) //A clickable clue is something the player can't pickup but must interact with as a clue (stone, magic book etc.)
    {
        Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>(); //Returns the interactable component on the gameobject hit

        if (distance <= interactable.radius) //Checks if its within range
        {
            clueManager.ClueFound(interactable.interactableName);
        }
    }
}
