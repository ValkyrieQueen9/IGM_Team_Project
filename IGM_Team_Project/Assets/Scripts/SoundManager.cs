using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource batsy; 
    public AudioSource PageTurn; 
    public AudioSource Click; 
    public AudioSource GrandfatherClock; 


    // Start is called before the first frame update
 public void PlayBatsy()
 {
    batsy.Play(); 
 }

  public void PlayPageTurn()
 {
    PageTurn.Play(); 
 }

  public void PlayClick()
 {
    Click.Play(); 
 }

  public void PlayGrandFatherClock()
 {
    GrandfatherClock.Play(); 
 }



}
