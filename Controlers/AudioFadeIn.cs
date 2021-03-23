using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioFadeIn : MonoBehaviour
{
    public AudioSource sound;
    
    public int seconds_to_fade = 10;

    private void Start()
    {
        sound.volume = 0;
    }
    void FixedUpdate()
    {
        if (sound.volume < 0.02f)
        {
            sound.volume = sound.volume + (Time.deltaTime / (seconds_to_fade + 1));
        }
    }
}
