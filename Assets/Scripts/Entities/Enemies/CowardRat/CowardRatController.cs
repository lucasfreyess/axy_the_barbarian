using UnityEngine;


public class CowardRatController : GameEntity
{
    private Vector2 moveDirection = Vector2.zero;


    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

    public void SetMoveDirection(Vector2 targetMoveDirection)
    {
        this.moveDirection = targetMoveDirection; // D = Df
    }
}
