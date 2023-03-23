using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private float HeroSpeed; 
      protected Vector2 direction;
      private Rigidbody2D myRigidBody; 
    // Start is called before the first frame update
   protected virtual void Start()
    { 
        myRigidBody = GetComponent<Rigidbody2D>(); 

        
    }

    // Update is called once per frame
    // I separeted it in case we want to update it differently 
   protected virtual void Update()
    {
        Move();

        
    } 
        // Move function that moves in the scene based on the direction given 
        // the direction is created bv the player. 
        public void Move(){

         transform.Translate(direction*HeroSpeed*Time.deltaTime); 
       



    }
}