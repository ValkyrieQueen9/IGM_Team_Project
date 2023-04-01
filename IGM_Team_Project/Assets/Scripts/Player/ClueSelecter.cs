using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueSelecter : MonoBehaviour
{

    /*
    // a reference to the clues that are associated between them.  
    is is an option to check if they are in the player posession but it ended up being harder to do 
    [SerializeField]
    GameObject InkClue; 
    [SerializeField]
    GameObject CircleClue; 
    [SerializeField]
    GameObject StoneClue; 
    [SerializeField]
    GameObject SwordClue; 

    */


    bool InkCircleActivted = false; 
    bool SwordInStoneActivated = false;
    bool hasInk = false; 
    bool hasStone = false; 
    bool hasSword = false;
    bool hasCircle = false; 

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //  InventoryChecker(); 
    }
        
/*
        
        if(hasInk == true && hasCircle == true) { 
            InkCircleActivted == true; 
            InkcirclePuzzletext();  Basically call the UI section that has the text of them solving the puzzle. 
            } 
        if(hasStone == true && hasSword == true) { 
            SwordInStoneActivated == true; 
            SwordInStoneActivated();  Basically call the UI section that has the text of them solving the puzzle. 
            } 

            ELSE
            RETURN.     
            this is the base logic of this section just copy pasted for every the other pair 

        }

        
        
    }
// this one checks the full inventory
    public void InventoryChecker() { 
        if(inventoryList.contains(ink)){
            hasInk == true; 
        }
         if(inventoryList.contains(sword)){
            hasSword == true; 
        }
         if(inventoryList.contains(circle)){
            hasCircle == true; 
        }
         if(inventoryList.contains(stone)){
            hasStone == true; 
        }

    }     */ 
}
