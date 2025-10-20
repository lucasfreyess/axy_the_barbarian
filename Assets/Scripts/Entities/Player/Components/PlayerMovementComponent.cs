using UnityEngine;


public class PlayerMovementComponent : MovementComponent
{
    [SerializeField] private PlayerController player;

    public override void Start()
    {
        transform.Translate(new Vector2(player.startingX, player.startingY));
    }
}
