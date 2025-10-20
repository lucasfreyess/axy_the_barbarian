using UnityEngine;


public class SkeletonController : GameEntity
{
    [Header("Componentes Particulares")]
    [SerializeField] private SkeletonShootingComponent shooting;
    [SerializeField] private SkeletonTimingComponent timing;

    protected override void Start()
    {
        //suscribe HandleShootReady al evento en TimingComponent
        timing.OnShootReady += HandleShootReady; 
    }

    protected override void Update()
    {
        timing.UpdateTimer();
    }

    // cuando el evento en TimingComponent es invocado, se llama automaticamente a esta funcion!
    private void HandleShootReady()
    {
        shooting.ShootArrow();
    }
}