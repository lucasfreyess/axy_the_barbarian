using UnityEngine;


public class AudioComponent : MonoBehaviour
{
    [SerializeField] protected AudioSource audioSource;

    public virtual void Start() {}
    
    public virtual void PlaySound()
    {
        if (audioSource != null) audioSource.Play();
    }
}