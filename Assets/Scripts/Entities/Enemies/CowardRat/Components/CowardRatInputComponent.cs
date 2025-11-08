using NUnit.Framework;
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
    public float fleeDistanceRadius = 5f;                            // distancia, respecto al jugador, en la que la rata comienza a flee-ear (o a seek-ear si es de noche!!)
    
    [Header("Attack-Propiedades")]
    [SerializeField] private float attackSpeed = 15f;                // v (magnitud de velocidad) en attack
    
    public Transform playerTransform;
    private float currentSpeed; // va cambiando entre wanderSpeed, fleeSpeed y attackSpeed !!!

    // decision-tree cosas
    private ObjectDecisionNode rootNode; // nodo inicial de Decision Tree de la rata
    private const string WANDER_ACTION_NAME = "Wander";
    private const string FLEE_ACTION_NAME = "Flee";
    private const string ATTACK_ACTION_NAME = "Attack";


    public override void Start()
    {
        base.Start();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
        Vector2 targetMoveDirection; // Df

        /*========================== COMIENZO DE DECISION MAKING =========================*/

        // rata no toma decisiones segun el mundo (aparte del jugador) todavia
        ActionNode resultNode = (ActionNode) rootNode.Decide(this.gameObject, new GameObject());

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

        /*========================== FIN DE DECISION MAKING =========================*/

        rat.SetMoveDirection(targetMoveDirection);
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

    private void ProcessTargetVelocity()
    {
        Vector2 moveDirection = rat.GetMoveDirection();    // Df (targetDirection)
        rat.SetMoveVelocity(currentSpeed * moveDirection); // Vf = v * Df
    }

    private void InitializeDecisionTree()
    {
        // evaluadores
        IsPlayerInsideRadiusEvaluator radiusEvaluator = new IsPlayerInsideRadiusEvaluator();

        // decisiones
        ObjectDecisionNode isPlayerInsideRadius = new ObjectDecisionNode(radiusEvaluator); // objectDecisionNode hace radiusEvaluator.Evaluate dentro de si
        //BoolDecisionNode isItNight = new BoolDecisionNode();

        // acciones
        ActionNode wanderAction = new ActionNode(WANDER_ACTION_NAME);
        ActionNode fleeAction = new ActionNode(FLEE_ACTION_NAME);
        ActionNode attackAction = new ActionNode(ATTACK_ACTION_NAME);

        // construir el arbol!!!
        isPlayerInsideRadius.NoNode = wanderAction;
        isPlayerInsideRadius.YesNode = fleeAction;
        //isPlayerInsideRadius.YesNode = isItNight;

        //isItNight.NoNode = fleeAction;
        //isItNight.YesNode = attackAction;

        // definir root
        rootNode = isPlayerInsideRadius;
    }
}
