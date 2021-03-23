using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioFadeOut : MonoBehaviour
{
    public AudioSource sound;
    public int seconds_to_fade = 5;

    IEnumerator FadeOut()
    {
        // Find Audio Music in scene
        // Check Music Volume and Fade Out
        while (sound.volume > 0f)
        {
            sound.volume -= Time.deltaTime / seconds_to_fade;
            yield return null;
        }

        // Make sure volume is set to 0
        sound.volume = 0;

        // Stop Music
        sound.Stop();

    }
}