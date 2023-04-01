using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactable : MonoBehaviour
{
    /* 
     * Attached to each interactable Game Object
     * In inspector:
     * - add the gameobjects name and radius
     * - add the items info, this is what will pop up when the item is clicked
     * - toolbarIcon is a UI Image gameObject which is different from a normal gameobject sprite - I have made these as prefabs in Prefabs > Items > ToolbarIcons 
     * - cluePopUp options can be found under UI > CluePopUps (renamed from ItemInfo)
     * - The options are small pop up for small items like a key, a big pop up for things like books with more info and ClueQuestions for things like the stone and magic circle.
    */

    public string interactableName;
    public float radius;
    public string itemInfo;
    public GameObject toolbarIcon;
    public GameObject cluePopUp;

    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
   
    private void OnDrawGizmosSelected() 
        /*
         *  This adds a circle around the game object so we can see the radius as we're making the scene. 
         *  When a player enters the circle radius they can interact with the game object.
         *  If you can't see it, in the scene view click on Gizmos in the top right.
         */
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Pickup()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                inventory.inventoryList.Add(interactableName);
                Debug.Log("Adding "+ interactableName +" to inventory");
                inventory.isFull[i] = true;
                Instantiate(toolbarIcon, inventory.slots[i].transform, false);
                Destroy(gameObject);
                break;
            }
            /*
            else if (inventory.isFull[i])
            {
                Debug.Log("Inventory slot is full");
                break;
            }
            */

        }

    }

    public void OpenDoor()
    {

    }

    
}
