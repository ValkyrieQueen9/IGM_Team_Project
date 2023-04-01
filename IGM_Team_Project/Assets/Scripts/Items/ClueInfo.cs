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

    public bool infoIsVisible;
    public GameObject[] cluePopUps;
    public TextMeshProUGUI[] questionTextBoxes;

    private void Start()
    {
        questionTextBoxes = cluePopUps[2].GetComponentsInChildren<TextMeshProUGUI>(); //Finds all the text components under the clue question game object
    }

    /* 
    *  Gets the text component on the information PopUp UI
    *  Sets the text content as the interactables text component
    *  Sets the popup as visible
    *  This will be changed when more clues come in with more complex information
    */


    public void CluePopUp(GameObject popUp, string info)
    {
        TextMeshProUGUI infoTextBox = popUp.GetComponentInChildren<TextMeshProUGUI>();
        infoTextBox.text = info;
        popUp.SetActive(true);
        infoIsVisible = true;
    }

    public void ClueQuestionPopUp(string questionText, string answer1Text, string answer2Text)
    {
        questionTextBoxes[0].text = questionText;
        questionTextBoxes[1].text = answer1Text;
        questionTextBoxes[2].text = answer2Text;
        cluePopUps[2].SetActive(true);
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
