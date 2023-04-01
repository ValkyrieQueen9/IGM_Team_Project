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

    public ClueInfo clueInfo;

    Camera cam;
    Inventory playerInventory;
    GameObject gameManagerObj;
    List<string> ClickableTags = new List<string>();

    [SerializeField]
    private Transform attackArea; 


    void Start()
    {
        cam = Camera.main;
        playerInventory = gameObject.GetComponent<Inventory>();
        gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");

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
        if (!clueInfo.infoIsVisible && Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition)); //Returns the gameobject collider the mouse clicks

            if (hit.collider != null && ClickableTags.Contains(hit.collider.gameObject.tag)) //Checks if a collider was hit and if that collider has a gameobject tag from the list
            {
                float distance = Vector2.Distance(gameObject.transform.position, hit.transform.position); //Returns the distance between the player and the gameobject collider
                //Debug.Log("you hit something");

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

        else if (clueInfo.infoIsVisible && Input.GetMouseButtonDown(0))
        {
            //Debug.Log("close popup");
            //If clue text is open and left mouse is clicked = Close Clue
            clueInfo.ClosePopUp();
        }
    }

    private void ClickableItem(RaycastHit2D hit, float distance) // Something the player can put in their inventory and will need to unlock a clue (key, ink etc.)
    {
        Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>(); //Returns the interactable component on the gameobject hit

        if (distance <= interactable.radius) //Checks if its within range
        {
            interactable.Pickup(); //puts in inventory
            clueInfo.CluePopUp(interactable.cluePopUp,  interactable.itemInfo); //Shows the items information/clue
        }
    }


    /*
     * This section might get very long and confusing when all the clue information is added.
     * Because we might be using two pieces of clue information (with and without the sword/ink), the itemInfo string in the interactable script might not work so well anymore.
     * I'm wondering whether we should make a new script like ClueSelector which can check the players inventory and trigger all the clue pop ups and trigger the games win?
     * We can also add the story writing straight into the script which could make bigger chunks of writing easier to look through.
     * 
     * It could look like this in this script:
     *      if (distance <= interactable.radius)
     *      {
     *          ClueSelector.ClueFound(interactable.name);
     *      }
     *      
     * This would use a new script and method and could look like this:
     *      private void ClueFound(string clueName)
     *      {
     *          if (clueName == "Stone") //Checks which clue it is
     *          {
     *              if(playerInventory.inventoryList.Contains("Sword")) //check if player has sword
     *              {
     *                  //show clue with options? - without options can just be a bigClue, strings used for itemInfo can be created in this new script?
     *                  
     *                  string stoneClueCompleteInfo = "A stone"; //Add all the writing here for each clue rather than in the inspector?
     *                  string stoneClueAnswer1 = "Put the sword in the slot";
     *                  string stoneClueAnswer2 = "do nothing";
     *                  clueInfo.ClueQuestionPopUp(stoneClueCompleteInfo, stoneClueAnswer1, stoneClueAnswer2);
     *              }
     *              else
     *              {
     *                  //show clue telling the player to search more
     *              }
     *          }
     *      }
     * 
     * For the CluePopUps I will create a new gameobject to use and a new method in ClueInfo that has more parameters for the information.
     * I'm not sure this will make much sense but I have the idea in my head and will probably forget it in the morning so I thought it best to write it down!
     */

    private void ClickableClue(RaycastHit2D hit, float distance) //A clickable clue is something the player can't pickup but must interact with as a clue (stone, magic circle etc.)
    {
        Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>(); //Returns the interactable component on the gameobject hit

        if (distance <= interactable.radius) //Checks if its within range
        {
            Debug.Log("Found a clue");
            if (interactable.interactableName == "Stone") //Checks which clue it is
            {
                if(playerInventory.inventoryList.Contains("Sword")) //check if player has sword
                {
                    Debug.Log("You put the sword in the stone");
                    //show clue with options?
                }
                else
                {
                    //show clue telling the player to search more
                }
            }
            if (interactable.interactableName == "Clock")
            {
                //don't need to check if player has the book? Just ask player what time to set
            }
            if (interactable.interactableName == "MagicCircle")
            {
                //check if player has the ink
            }

            if (interactable.interactableName == "Door")
            {
                //Checks if the player has a key in their inventory
                if (playerInventory.inventoryList.Contains("Key"))
                {
                    gameManagerObj.GetComponent<Menus>().WinGame();
                }

                else
                {
                    clueInfo.CluePopUp(interactable.cluePopUp, interactable.itemInfo);
                }
            }
        }
    }
}
