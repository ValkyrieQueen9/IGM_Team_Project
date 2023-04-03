using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// I used monobehaviour but in my original version it was dependand on the character class to make top down changes
public class PlayerMovement : Character
{
    public string playerLocation;
    string currentScene;
    bool playerDied = false;
    private Animator animator;
    private Menus menuManager;
    public ClueManager clueManager;
    public GameObject smallClue;

   // protected Vector2 direction;
   protected override void Start()
    {
        base.Start();
        currentScene = SceneManager.GetActiveScene().name;
        animator = GetComponent<Animator>();
        menuManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Menus>();
    }

    protected override void Update()
    {

        if (!menuManager.IsGamePaused)
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
        }
        else
        {
            direction = Vector2.zero;
            animator.SetBool("IsWalking", false); // says if character is Not moving for animator
        }

        if (playerDied)
        {
            if (!clueManager.infoIsVisible)
            {
                SceneManager.LoadScene("MainMenu");
            }
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
            clueManager.CluePopUp(smallClue, "You Died! The creature caught up to you!");
            menuManager.IsGamePaused = true;
            playerDied = true;
        }

        if(collision.gameObject.tag == "Room")
        {
            playerLocation = collision.gameObject.name;
        }

        if(ClueManager.completedClues >= 3 && collision.gameObject.tag == "Door")
        {
            SceneManager.LoadScene("WinMenu");
        }



    }

}
