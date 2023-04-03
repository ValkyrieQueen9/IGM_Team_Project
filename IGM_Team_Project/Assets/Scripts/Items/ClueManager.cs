using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClueManager : MonoBehaviour
{

    /* Script Plan
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

    public string currentClue;
    public bool infoIsVisible;
    public bool questionIsVisible;
    public static int completedClues;
    public GameObject[] cluePopUps; // [0] is the small popup, [1] is the big popup, [2] is the question popup

    TextMeshProUGUI[] questionTextBoxes; // [0] is the main question text, [1] is answer 1/the left button, [2] is answer 2/the right button
    List<string> completedCluesList = new List<string>();
    bool answer1Selected;
    bool answer2Selected;
    bool shownLastNote = false;

    [SerializeField]
    GameObject batsy;
    int batsySpawnRoom;
    bool hasBatsyAppeared;
    public GameObject[] batsySpawnPos;
    Inventory playerInventory;
    PlayerMovement playerMovement;

    #region All ClueQuestionText
    //Number 1 is always the yes answer, 2 is always the no answer

    //Stone question and answers
    string stoneQuestion = "On a small podium sits a carved, stone-like object. It has a slot in the middle, that letter opener might fit in there.";
    string stoneHint = "On a small podium sits a carved, stone-like object. It has a slot in the middle, is there something that could fit in there?";
    string stoneAnswer1 = "Use the letter opener";
    string stoneAnswer2 = "Do nothing";
    string stoneClueResult = "You insert the letter opener in the slot in the stone and a note slips out from under the podium: ‘They promised me that I’d be able to escape, instead here they are, trapped alongside me. I just hope my fiance can defeat them, or at least escape.’";

    //MagicBook question and answers
    string magicBookQuestion = "You find an old book with torn pages and curious markings on the spine. The pages of the book are empty but the glass bottle of ink you found glows in its presence.";
    string magicBookHint = "You find an old book with torn pages and curious markings on the spine. The pages of the book are empty but seem to shine strangely in the light as if they’re hiding something within.";
    string magicBookAnswer1 = "Use the ink";
    string magicBookAnswer2 = "Do nothing";
    string magicBookClueResult = "The book glows red and letters appear floating in the air reading: ‘You tried to bind me, but now I will devour anyone alive in this place’ ";

    //Clock question and answers
    string clockQuestion = "It’s an old clock, but the hands are broken and pointing down. Below them, a mechanism drips with a black oily substance. Do you want to set up a specific time? ";
    string clockAnswer1 = "Set at 12:00";
    string clockAnswer2 = "Set at 5:00";
    string clockClueResult = "I tried, but I couldn’t stop it. So I did the next best thing and bound them to this place";
    string clockClueWrong = "The clock hands tick once and fall back down";

    string allCluesFound = "You hear a noise outside as an oily black substance forms on the floor and a bat-like creature appears in shadow. You feel a chill down your spine as the creature approaches you. Can you make it to the door in time?";
    #endregion


    private void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        questionTextBoxes = cluePopUps[2].GetComponentsInChildren<TextMeshProUGUI>(); //Finds all the text components under the clue question game object
        //batsySpawnPos = GameObject.FindGameObjectsWithTag("BatsySpawn");
        completedClues = 0;
        hasBatsyAppeared = false;
    }

    private void Update()
    {
        if (questionIsVisible && answer1Selected)
        {
            if(currentClue == "Stone")
            {
                ClueCompleted(currentClue, stoneClueResult);
            }

            if (currentClue == "MagicBook")
            {
                ClueCompleted(currentClue, magicBookClueResult);
            }

            if (currentClue == "Clock")
            {
                ClueCompleted(currentClue, clockClueResult);
            }
        }

        if (questionIsVisible && answer2Selected)
        {
            if(currentClue == "Clock")
            {
                ClosePopUp();
                currentClue = "Clock";
                CluePopUp(cluePopUps[0], clockClueWrong);
                answer1Selected = false;
                answer2Selected = false;
            }
            else
            {
                ClosePopUp();
                answer1Selected = false;
                answer2Selected = false;
            }
        }

        if(completedClues >= 3)
        {
            AllCluesCompleted();
        }
    }

    public void CluePopUp(GameObject popUp, string info)
    {
        /* 
        *  Gets the text component on the Clue PopUp UI
        *  Sets the text content as the interactables text component
        *  Sets the popup as visible
        */
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
        questionIsVisible = true;
        cluePopUps[2].SetActive(true);
    }

    public void ClosePopUp()
    {
        foreach (GameObject c in cluePopUps)
        {
            c.SetActive(false);
        }

        currentClue = null;
        infoIsVisible = false;
        questionIsVisible = false;
    }

    public void ClueFound(string clueName) //Triggered in the playerInteract script when a clue is clicked
    {
        if (!completedCluesList.Contains(clueName))
        {
            if (clueName == "Stone") //Checks which clue it is
            {
                currentClue = clueName; //sets the current clue

                if (playerInventory.inventoryList.Contains("Sword")) //check if player has sword
                {
                    ClueQuestionPopUp(stoneQuestion, stoneAnswer1, stoneAnswer2); //Asks if the player wants to use the sword or not
                }
                else
                {
                    CluePopUp(cluePopUps[0], stoneHint); //Tells the player a hint
                }
            }

            if (clueName == "MagicBook")
            {
                currentClue = clueName;

                if (playerInventory.inventoryList.Contains("Ink"))
                {
                    ClueQuestionPopUp(magicBookQuestion, magicBookAnswer1, magicBookAnswer2);
                }
                else
                {
                    CluePopUp(cluePopUps[0], magicBookHint);
                }
            }

            if (clueName == "Clock")
            {
                currentClue = clueName;
                ClueQuestionPopUp(clockQuestion, clockAnswer1, clockAnswer2);

            }
        }
        else
        {
            Debug.Log("You've done that clue");
        }
    }

    public void ClueCompleted(string clueName, string clueResult)
    {
        completedCluesList.Add(clueName);
        ClosePopUp();
        CluePopUp(cluePopUps[1], clueResult);
        answer1Selected = false;
        answer2Selected = false;
        completedClues += 1;
    }

    public void AllCluesCompleted()
    {
        completedClues = 3;
        if (!infoIsVisible && !shownLastNote)
        {
            CluePopUp(cluePopUps[1], allCluesFound);
            shownLastNote = true;
        }

        if(!infoIsVisible && hasBatsyAppeared == false)
        {
            if(playerMovement.playerLocation == "Hallway")
            {
                batsySpawnRoom = 1; //spawning in living room
            }
            else if (playerMovement.playerLocation == "Bedroom")
            {
                batsySpawnRoom = 2; //spawning in study
            }
            else if (playerMovement.playerLocation == "LivingRoom")
            {
                batsySpawnRoom = 0; //spawning in bedroom
            }
            else if (playerMovement.playerLocation == "Study")
            {
                batsySpawnRoom = 1; //spawning in living room
            }

            Debug.Log("Batsy incoming!");
            Instantiate(batsy, batsySpawnPos[batsySpawnRoom].transform);
            hasBatsyAppeared = true;
        }
    }

    public void answer1Button()
    {
        answer1Selected = true;
    }

    public void answer2Button()
    {
        answer2Selected = true;
    }

}
