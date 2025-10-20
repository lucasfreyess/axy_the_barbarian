using System;
using UnityEngine;


// no es necesario hacer una interfaz de TimingComponent pq solo hay un mono que cuenta el tiempo!!
public class SkeletonTimingComponent : MonoBehaviour
{
    [SerializeField] private float shootInterval = 1f; // no puede ser igual que la velocidad del Gazer
    private float shootIntervalTimer = 0f;
    public event Action OnShootReady;                  // evento que le avisa al controller q el ShootingComponent puede disparar!

    public void UpdateTimer()
    {
        // el unico estado interno (que se modifica actualmente) del esqueleto es el timer para disparar flechas!!
        shootIntervalTimer += Time.deltaTime;
        CheckTimer();
    }

    private void CheckTimer()
    {
        // checkear si el timer excedio el intervalo de disparos de flechas!
        if (shootIntervalTimer >= shootInterval)
        {
            Debug.Log("Timer excedio el intervalo!");
            shootIntervalTimer = 0f; // reiniciar el timer

            OnShootReady?.Invoke();
        }
    }
}