using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class PlayerAudioComponent : AudioComponent
{
    public void PlayBeepSound()
    {
        #if UNITY_EDITOR
        EditorApplication.Beep(); // sonido en el editor
        #endif

        PlaySound();
    }
    
    private void PlaySound()
    {
        if (audioSource != null) audioSource.Play();
    }
}