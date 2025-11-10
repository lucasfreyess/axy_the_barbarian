using UnityEngine;


// funge como el componente de IA de la rata
public class CowardRatInputComponent : InputComponent
{
    [SerializeField] private CowardRatController rat;
    public float fleeOrAttackRadius = 10f;                           // distancia, respecto al jugador, en la que la rata comienza a flee-ear (o a seek-ear si es de noche!!)

    [Header("Wander-Propiedades")]
    [SerializeField] private float wanderSpeed = 5f;                 // v (magnitud de velocidad) en idle
    [SerializeField] private float wanderAngleVariationRange = 0.1f; // rango de variacion chica del angulo para el moveDirection en cada frame

    [Header("Flee-Propiedades")]
    [SerializeField] private float fleeSpeed = 25f;                  // v (magnitud de velocidad) en flee

    [Header("Attack-Propiedades")]
    [SerializeField] private float attackSpeed = 10f;                // v (magnitud de velocidad) en attack

    [Header("Obstacle Avoidance")]
    [SerializeField] private bool shouldAvoidObstacles = true;       // para confirmar en runtime q si se estan evadiendo las paredes
    [SerializeField] private float avoidanceRayLength = 5f;          // longitud del raycast
    [SerializeField] private float avoidanceForce = 2f;            // que tanto empuja lateralmente
    [SerializeField] private LayerMask obstacleLayer;                // capas con layer Obstacle (walls y esqueleto)
    

    [HideInInspector] public Transform playerTransform;
    private GameObject lightingManagerObject;                        // para obtener isItNight
    private float currentSpeed;                                      // va cambiando entre wanderSpeed, fleeSpeed y attackSpeed !!!

    // decision-tree cosas
    private ObjectDecisionNode rootNode;                             // nodo inicial de Decision Tree de la rata
    private const string WANDER_ACTION_NAME = "Wander";
    private const string FLEE_ACTION_NAME = "Flee";
    private const string ATTACK_ACTION_NAME = "Attack";


    public override void Start()
    {
        base.Start();

        // obtener transform del jugador
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        // obtener objeto que posee el script de dayNightCycle
        lightingManagerObject = GameObject.Find("LightingManagerObject");
        if (lightingManagerObject == null) Debug.LogWarning("GameObject 'LightingManagerObject' no fue encontrado");
        
        InitializeDecisionTree();
    }

    public override void ProcessInput()
    {
        if (playerTransform == null) return;

        ProcessTargetMoveDirection(); // para modificar el vector de direccion
        ProcessTargetVelocity();      // para modificar el vector de velocidad
    }

    private void ProcessTargetMoveDirection()
    {
        // Decision Making mediante el Decision Tree!!
        Vector2 targetMoveDirection = EvaluateDecisionTree();

        // modificar orientacion si es q se va a chocar con un obstaculo!!
        if (shouldAvoidObstacles) targetMoveDirection = ApplyObstacleAvoidance(targetMoveDirection);

        // setear orientacion segun output de decision making
        rat.SetMoveDirection(targetMoveDirection);
    }

    private void InitializeDecisionTree()
    {
        // evaluadores
        IsPlayerInsideRadiusEvaluator radiusEvaluator = new IsPlayerInsideRadiusEvaluator();
        IsItNightEvaluator nightEvaluator = new IsItNightEvaluator();

        // decisiones
        ObjectDecisionNode isPlayerInsideRadius = new ObjectDecisionNode(radiusEvaluator); // objectDecisionNode hace radiusEvaluator.Evaluate dentro de si
        ObjectDecisionNode isItNight = new ObjectDecisionNode(nightEvaluator);

        // acciones
        ActionNode wanderAction = new ActionNode(WANDER_ACTION_NAME);
        ActionNode fleeAction = new ActionNode(FLEE_ACTION_NAME);
        ActionNode attackAction = new ActionNode(ATTACK_ACTION_NAME);

        // construir el arbol!!!
        isPlayerInsideRadius.NoNode = wanderAction;
        isPlayerInsideRadius.YesNode = isItNight;

        isItNight.NoNode = fleeAction;
        isItNight.YesNode = attackAction;

        // definir root
        rootNode = isPlayerInsideRadius;
    }

    // metodo para hacer decision making xd
    private Vector2 EvaluateDecisionTree()
    {
        ActionNode resultNode = (ActionNode)rootNode.Decide(this.gameObject, lightingManagerObject);
        Vector2 targetMoveDirection; // Df

        switch (resultNode.name)
        {
            case WANDER_ACTION_NAME:
                targetMoveDirection = ProcessWanderMovement();
                break;

            case FLEE_ACTION_NAME:
                targetMoveDirection = ProcessFleeMovement();
                break;

            case ATTACK_ACTION_NAME:
                targetMoveDirection = ProcessAttackMovement();
                break;

            default:
                targetMoveDirection = ProcessWanderMovement();
                break;
        }

        return targetMoveDirection;
    }

    private Vector2 ProcessWanderMovement()
    {
        // calculo de small_random_delta de angulo
        Vector2 randomAngleVariation = new(
            Random.Range(-wanderAngleVariationRange, wanderAngleVariationRange),
            Random.Range(-wanderAngleVariationRange, wanderAngleVariationRange)
        );

        currentSpeed = wanderSpeed;
        return (rat.GetMoveDirection() + randomAngleVariation).normalized; // Df = Di + small_random_delta  
    }

    private Vector2 ProcessFleeMovement()
    {
        // asigno las siguientes variables para que quede claro cual es Pf (targetPosition) y cual es Pi (actualPosition)
        Vector2 actualPosition = this.transform.position;
        Vector2 targetPosition = playerTransform.position;

        currentSpeed = fleeSpeed;
        return -(targetPosition - actualPosition).normalized; // Df = -N(Pf-Pi)
    }

    private Vector2 ProcessAttackMovement()
    {
        // copy-paste de ProcessFleeMovement; simplemente hace seek xd

        // asigno las siguientes variables para que quede claro cual es Pf (targetPosition) y cual es Pi (actualPosition)
        Vector2 actualPosition = this.transform.position;
        Vector2 targetPosition = playerTransform.position;

        currentSpeed = attackSpeed;
        return (targetPosition - actualPosition).normalized; // Df = N(Pf-Pi) (seek)
    }

    private void ProcessTargetVelocity()
    {
        Vector2 moveDirection = rat.GetMoveDirection();    // Df (targetDirection)
        rat.SetMoveVelocity(currentSpeed * moveDirection); // Vf = v * Df
    }
    
    private Vector2 ApplyObstacleAvoidance(Vector2 currentDirection)
    {
        Vector2 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, currentDirection, avoidanceRayLength, obstacleLayer);

        if (hit.collider == null) return currentDirection;

        // Dibujar para depuración (línea roja)
        Debug.DrawLine(origin, hit.point, Color.red);

        // calcular dirección perpendicular a la dirección actual
        Vector2 perpendicularDirection = Vector2.Perpendicular(currentDirection).normalized;

        // Elegir el lado que se aleja más del obstáculo
        if (Vector2.Dot(perpendicularDirection, hit.normal) < 0) 
            perpendicularDirection = -perpendicularDirection;

        // Combinar dirección original + dirección de evasión
        Vector2 avoidanceDirection = (currentDirection + perpendicularDirection * avoidanceForce).normalized;

        return avoidanceDirection;
    }
}
