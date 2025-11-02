using UnityEngine;


public class CowardRatController : GameEntity
{
    private Vector2 moveDirection = Vector2.zero; // vector de direccion actual (D)
    private Vector2 moveVelocity = Vector2.zero;  // vector de velocidad actual (V)


    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    // ===== getters y setters ======
    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

    public void SetMoveDirection(Vector2 targetMoveDirection)
    {
        this.moveDirection = targetMoveDirection; // D = Df
    }

    public Vector2 GetMoveVelocity()
    {
        return moveVelocity;
    }

    public void SetMoveVelocity(Vector2 targetMoveVelocity)
    {
        this.moveVelocity = targetMoveVelocity; // V = Vf
    }    
}
