using UnityEngine;


public class CowardRatMovementComponent : MovementComponent
{
    [SerializeField] private CowardRatController rat;
    [SerializeField] private float speed = 5f; // v (magnitud de velocidad)


    public override void UpdateState()
    {
        Vector2 moveDirection = rat.GetMoveDirection(); // Df (targetDirection)
        Vector2 targetVelocity = speed * moveDirection; // Vf = v * Df
        //Debug.Log("CowardRatMovementComponent: targetVelocity calculated as " + targetVelocity);
        transform.Translate(targetVelocity * Time.deltaTime);
    }
}
