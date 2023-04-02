using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthAndAttack : MonoBehaviour
{
    public float BatHitpoints;
    [SerializeField]
    private float MaxBatHitpoints = 12 ; 
    PlayerHealth playerHealth; // we reference the script to get the damage health function


    // intitlaize the hitpoints 

    void Start()
    {
        BatHitpoints = MaxBatHitpoints;    
    }

    // for the future once i get the damage attack function, it gives damage to the bat. 
    public void TakeHit(float damage)
    {
        BatHitpoints -= damage; 
        if (BatHitpoints <= 0 )
        {
            Destroy (gameObject); 
        }
 
    }
 //this is the section to give damage to the player 


}
