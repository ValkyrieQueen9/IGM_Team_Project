using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> inventoryList = new List<string>(); //Used for checking inventory with clues later
    public bool[] isFull;
    public GameObject[] slots;

    private void Start()
    {
        slots = GameObject.FindGameObjectsWithTag("InventorySlot");
        inventoryList.Add("AmeliaSword");
    }

}


