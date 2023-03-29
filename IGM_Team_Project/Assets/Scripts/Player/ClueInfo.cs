using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClueInfo : MonoBehaviour
{
    /* 
     * Will hold all the methods for popups
     * Eventually might hold popup controls for doors, bigger clues or dialogue
     */

    public GameObject[] cluePopUps;
    public bool infoIsVisible;

    public void CluePopUp(GameObject popUp, string info)
    {
        /* 
        *  Gets the text component on the information PopUp UI
        *  Sets the text content as the interactables text component
        *  Sets the popup as visible
        *  This will be changed when more clues come in with more complex information
        */

        TextMeshProUGUI infoTextBox = popUp.GetComponentInChildren<TextMeshProUGUI>();
        infoTextBox.text = info;
        popUp.SetActive(true);
        infoIsVisible = true;
    }

    public void ClosePopUp()
    {
        foreach (GameObject c in cluePopUps)
        {
            c.SetActive(false);
        }
        infoIsVisible = false;
    }
}
