using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactable : MonoBehaviour
{
    /* Attached to the UI Item Buttons
    *  When an item button is clicked the code checks the inventory slots to find an available place to put it.
    *  Currently using buttons as a way to interact with items until interact code is done
    */

    public GameObject item;
    public float radius;
    public string itemInfo;
    public GameObject cluePopUp;

    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
   
    private void OnDrawGizmosSelected()
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
                inventory.inventoryList.Add(item);
                Debug.Log("Adding Item to Inventory");
                inventory.isFull[i] = true;
                Instantiate(item, inventory.slots[i].transform, false);
                Destroy(gameObject);
                break;
            }
            else
            {
                //somethings not working here
                Debug.Log("Inventory Full");
            }

        }
    }

    public void OpenDoor()
    {

    }

    
}
