using UnityEngine;


// funge como el componente de IA de la rata
public class CowardRatInputComponent : InputComponent
{
    [SerializeField] private CowardRatController rat;
    [SerializeField] private float fleeDistanceRadius = 5f;          // distancia, respecto al jugador, en la que la rata comienza a flee-ear
    [SerializeField] private float wanderAngleVariationRange = 0.1f; // rango de variacion chica del angulo para el moveDirection en cada frame

    private Transform playerTransform;


    public override void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //Debug.Log("CowardRatInputComponent: playerTransform assigned to " + playerTransform.name);
    }

    public override void ProcessInput()
    {
        if (playerTransform == null) return;

        ProcessTargetMoveDirection(); // para modificar el moveDirection
        ProcessTargetVelocity();      // para modificar la velocidad
    }

    private void ProcessTargetMoveDirection()
    {
        float distanceToPlayer = Vector2.Distance(playerTransform.position, this.transform.position);
        Vector2 targetMoveDirection; // Df

        // si jugador esta dentro del radio de flee de la rata, se flee-ea; e.o.c. (idly), se wander-ea
        if (distanceToPlayer <= fleeDistanceRadius) targetMoveDirection = ProcessFleeMovement();
        else targetMoveDirection = ProcessWanderMovement();

        rat.SetMoveDirection(targetMoveDirection);
        //Debug.Log("CowardRatInputComponent: moveDirection set to " + targetMoveDirection);
    }

    private void ProcessTargetVelocity()
    {
        // velocidad es manejada por el CowardRatMovementComponent por ahora...... lo quiero cambiar!!
    }

    private Vector2 ProcessFleeMovement()
    {
        // asigno las siguientes variables para que quede claro cual es Pf (targetPosition) y cual es Pi (actualPosition)
        Vector2 actualPosition = this.transform.position;
        Vector2 targetPosition = playerTransform.position;

        return -(targetPosition - actualPosition).normalized; // Df = -N(Pf-Pi)
    }

    private Vector2 ProcessWanderMovement()
    {
        Vector2 randomAngleVariation = new(
            Random.Range(-wanderAngleVariationRange, wanderAngleVariationRange),
            Random.Range(-wanderAngleVariationRange, wanderAngleVariationRange)
        );

        return (rat.GetMoveDirection() + randomAngleVariation).normalized; // Df = Di + small_random_delta  
    }
}
