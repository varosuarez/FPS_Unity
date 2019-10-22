using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSounds : MonoBehaviour
{

    public AudioClip[] chickens;
    public AudioSource theChickenPlayer;
    // Start is called before the first frame update
    void Start()
    {
        theChickenPlayer = GetComponent<AudioSource>();
    }

    public void playSomeRandomChicken()
    {
        AudioClip aClip = chickens[UnityEngine.Random.Range(0, chickens.Length)];
        theChickenPlayer.clip = aClip;
        theChickenPlayer.Play();
    }
    
}
