using UnityEngine;


//componente usado solo para establecer la posicion inicial lol
public class SkeletonMovementComponent : MovementComponent
{
    [SerializeField] private SkeletonController skeleton;

    public override void Start()
    {
        // SkeletonArcher empieza en (8,0) y no se mueve
        transform.Translate(new Vector2(skeleton.startingX, skeleton.startingY));
    }
}