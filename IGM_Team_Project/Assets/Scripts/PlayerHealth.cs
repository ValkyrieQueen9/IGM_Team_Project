using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    /*
    * Basic player health script with UI slider bar(for now - can create hearts system instead) 
    * Currently the scene will restart when the player reaches 0 but this can change to a death and gameover/restart option later
    * The code for a damage sound are there when we need them later
    */

    [Header("Health Values")]
    public int currentHealth = 100;
    public int maxHealth = 100;

    [Header("Health Bar")]
    public Slider healthBar;
    public Image sliderFillImage;
    public Color healthBarRed;
    public Color healthBarYellow;
    public Color healthBarGreen;

    AudioSource damageSound;
    string currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        damageSound = gameObject.GetComponent<AudioSource>();

        //Gives the UI health bar the correct starting values
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        sliderFillImage.color = healthBarGreen;
    }

    private void Update()
    {

        #region Changes colour of health bar as it goes down

        //Gets rid of remaining health bar color when health is 0 - without this a small little bit of color stays on the health bar
        if (healthBar.value <= healthBar.minValue) 
        {
            sliderFillImage.enabled = false;
        }

        if (healthBar.value > healthBar.minValue && !sliderFillImage.enabled)
        {
            sliderFillImage.enabled = true;
        }

        //Changes the color of the health bar as it goes down
        if (currentHealth <= healthBar.maxValue / 3)
        {
            sliderFillImage.color = healthBarRed;
        }

        else if (currentHealth <= healthBar.maxValue * 2 / 3)
        {
            sliderFillImage.color = healthBarYellow;
        }

        else if (currentHealth == healthBar.maxValue)
        {
            sliderFillImage.color = healthBarGreen;
        }

        #endregion
    }


    public void DamageHealth(int damage) //Used by the enemy attack script to apply damage to the player
    {
        currentHealth = currentHealth - damage;

        healthBar.value = currentHealth;//Changes the UI health bar value

        //damageSound.Play();//Plays a damage sound using the audio source on the player character (no audio selected currently)
        Debug.Log("Health:" + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Restarting");
            SceneManager.LoadScene(currentScene);
        }
    }
}
