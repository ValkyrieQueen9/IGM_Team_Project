using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// I used monobehaviour but in my original version it was dependand on the character class to make top down changes
public class PlayerMovement : Character
{
    public string playerLocation;
    string currentScene;
    private Animator animator;


   // protected Vector2 direction;
    // Start is called before the first frame update
   protected override void Start()
    {
        base.Start();
        currentScene = SceneManager.GetActiveScene().name;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    protected override void Update()
    {
        //Executes the GetInput function
         GetInput();
        //getting the character directions to show the proper animations
        if (direction != Vector2.zero)
        {
           
            animator.SetFloat("Xinput", direction.x);
            animator.SetFloat("Yinput", direction.y);

            animator.SetBool("IsWalking", true); //says if character is moving for animator
     
        }
        else
        {
            animator.SetBool("IsWalking", false); // says if character is Not moving for animator
        }

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
    }

    private void OnTriggerEnter2D(Collider2D collision) //Used a trigger collision to fix the tilemap issue
    {
        if (collision.gameObject.tag == ("Enemy"))
        {
            Debug.Log("playerhit");
            SceneManager.LoadScene(currentScene);
        }

        if(collision.gameObject.tag == "Room")
        {
            playerLocation = collision.gameObject.name;
        }





    }

}
