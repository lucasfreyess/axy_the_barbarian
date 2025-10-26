using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class PlayerAudioComponent : AudioComponent
{
    [SerializeField] private PlayerPhysicsComponent physics;

    public override void Start()
    {
        GlobalListener.Instance.OnPlayerCollisionGlobal += PlayBeepSound;
    }

    private void PlayBeepSound()
    {
        #if UNITY_EDITOR
        EditorApplication.Beep(); // sonido en el editor
        #endif

        PlaySound();
    }
}