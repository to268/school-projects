using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    public AudioSource pressedAudio;
    public AudioSource selectedAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EffectPressed()
    {
        pressedAudio.Play();
    }

    void EffectSelected()
    {
        selectedAudio.Play();
    }
}
