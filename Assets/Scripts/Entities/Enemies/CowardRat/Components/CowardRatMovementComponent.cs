using UnityEngine;


public class CowardRatMovementComponent : MovementComponent
{
    [SerializeField] private CowardRatController rat;
    

    public override void UpdateState()
    {
        //Debug.Log("CowardRatMovementComponent: targetVelocity calculated as " + rat.GetMoveVelocity());
        transform.Translate(rat.GetMoveVelocity() * Time.deltaTime);
    }
}
