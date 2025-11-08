using System;
using UnityEngine;


// singleton para manejar notificaciones mediante eventos
public class GlobalListener : MonoBehaviour
{
    public static GlobalListener Instance { get; private set; } // getter es publico, setter es privado

    // eventos globales
    public event Action OnPlayerCollisionGlobal;  //para que el soundcomponent pueda reproducir sonido!
    public event Action OnGameWonGlobal;          //para imprimir texto de victoria/derrota en UI/TextWriter.cs
    public event Action OnGameLostGlobal;
    public event Action OnLevelsGenerated;

    private void Awake()
    {
        // asegurar que solo exista uno
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // persistencia entre escenas
        //DontDestroyOnLoad(gameObject);
    }

    // metodos públicos para invocar eventos
    public void NotifyPlayerCollision()
    {
        OnPlayerCollisionGlobal?.Invoke();
    }

    public void NotifyGameWon()
    {
        OnGameWonGlobal?.Invoke();
    }

    public void NotifyGameLost()
    {
        OnGameLostGlobal?.Invoke();
    }

    public void NotifyLevelsGenerated()
    {
        OnLevelsGenerated?.Invoke();
    }
}
