using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwarness : MonoBehaviour
{
        //To get if the bat is aware of the enemy, alternatively this can be reused for stealth
    public bool AwareOfPlayer {get; private set;} 
    // to get the direction of the player 
    public Vector2 DirectionToPlayer {get; private set;}
    // distance to see how far it can see  
    [SerializeField]
    private float _playerAwarenessDistance;
// location of the player 
    private Transform _player; 
    // to get the type of player 
    // Start is called before the first frame update

            private void Awake ()
    {
        // we get the movement of the player 
        _player = FindObjectOfType<PlayerMovement>().transform; 

    }


    // Update is called once per frame
    void Update()
    {
         // how far amelia is and the direction 
        Vector2 enemyToPlayerVector = _player.position - transform.position; 
        DirectionToPlayer = enemyToPlayerVector.normalized; // normalize to normal units 
        if( enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
        {
            AwareOfPlayer = true; 
        }
            else 
            {
                AwareOfPlayer = false; 

            }
        }
        
    }

