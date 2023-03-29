using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// I used monobehaviour but in my original version it was dependand on the character class to make top down changes
public class PlayerMovement : Character
{
   // protected Vector2 direction;
    // Start is called before the first frame update
   protected override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
   protected override void Update()
    {
        //Executes the GetInput function
         GetInput();
         base.Update();
         
        
    }

        private void GetInput()
    {
        direction = Vector2.zero;


        if (Input.GetKey(KeyCode.W)) //Moves up
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A)) //Moves left
        {
            direction += Vector2.left; //Moves down
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D)) //Moves right
        {
            direction += Vector2.right;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
           // StartCoroutine(Interact()); 
        }
        if (Input.GetKey(KeyCode.Mouse0)) 
        {
            // StartCoroutine(attack())
        }
    }

 
}
