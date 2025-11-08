using UnityEngine;


// funge como el componente de IA de la rata
public class CowardRatInputComponent : InputComponent
{
    [Header("Controlador")]
    [SerializeField] private CowardRatController rat;

    [Header("Wander-Propiedades")]
    [SerializeField] private float wanderSpeed = 5f;                 // v (magnitud de velocidad) en idle
    [SerializeField] private float wanderAngleVariationRange = 0.1f; // rango de variacion chica del angulo para el moveDirection en cada frame

    [Header("Flee-Propiedades")]
    [SerializeField] private float fleeSpeed = 25f;                  // v (magnitud de velocidad) en flee
    [SerializeField] private float fleeDistanceRadius = 5f;          // distancia, respecto al jugador, en la que la rata comienza a flee-ear
    
    private Transform playerTransform;
    private bool isFleeing; // para saber que velocidad aplicar en ProcessTargetVelocity()


    public override void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void ProcessInput()
    {
        if (playerTransform == null) return;

        ProcessTargetMoveDirection(); // para modificar el vector de direccion
        ProcessTargetVelocity();      // para modificar el vector de velocidad
    }

    private void ProcessTargetMoveDirection()
    {
        float distanceToPlayer = Vector2.Distance(playerTransform.position, this.transform.position);
        Vector2 targetMoveDirection; // Df

        // si jugador esta dentro del radio de flee de la rata, se flee-ea; e.o.c. (idly), se wander-ea
        if (distanceToPlayer <= fleeDistanceRadius)
            targetMoveDirection = ProcessFleeMovement();
        else
            targetMoveDirection = ProcessWanderMovement();

        rat.SetMoveDirection(targetMoveDirection);
    }

    private Vector2 ProcessFleeMovement()
    {
        // asigno las siguientes variables para que quede claro cual es Pf (targetPosition) y cual es Pi (actualPosition)
        Vector2 actualPosition = this.transform.position;
        Vector2 targetPosition = playerTransform.position;

        isFleeing = true;
        return -(targetPosition - actualPosition).normalized; // Df = -N(Pf-Pi)
    }

    private Vector2 ProcessWanderMovement()
    {
        // calculo de small_random_delta de angulo
        Vector2 randomAngleVariation = new(
            Random.Range(-wanderAngleVariationRange, wanderAngleVariationRange),
            Random.Range(-wanderAngleVariationRange, wanderAngleVariationRange)
        );

        isFleeing = false;
        return (rat.GetMoveDirection() + randomAngleVariation).normalized; // Df = Di + small_random_delta  
    }

    private void ProcessTargetVelocity()
    {
        Vector2 moveDirection = rat.GetMoveDirection(); // Df (targetDirection)
        float currentSpeed = isFleeing ? fleeSpeed : wanderSpeed;

        rat.SetMoveVelocity(currentSpeed * moveDirection); // Vf = v * Df
    }
}
