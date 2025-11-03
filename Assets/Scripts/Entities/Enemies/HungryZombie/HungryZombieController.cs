using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class HungryZombieController : MonoBehaviour
{
    [Header("Pathfinding")]
    //[SerializeField] private AStarAlgorithm aStar;
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float pathRecalculateRate = 0.5f; // para recalcular el camino 2 veces por segundo

    [Header("Propiedades")]
    [SerializeField] private Rigidbody2D rb;

    private Transform player;
    private float pathRecalculateTimer;
    private AStarAlgorithm aStar;
    
    // el camino que se sigue actualmente
    private List<GraphNode> currentPath;
    private int currentPathIndex;

    void Start()
    {
        // encontrar al jugador
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) player = playerObject.transform;
        
        // encontrar el script de aStar dentro de PathfindingObject
        GameObject pathfindingObject = GameObject.Find("PathfindingObject");
        if (pathfindingObject != null)
            aStar = pathfindingObject.GetComponent<AStarAlgorithm>();

        if (aStar == null)
            Debug.LogError("HungryZombie no pudo encontrar el AStarAlgorithm!");
        else Debug.Log("HungryZombie encontró el AStarAlgorithm, Nombre Objeto: " + aStar.gameObject.name);

        if (player == null)
            Debug.LogError("HungryZombie no pudo encontrar al Player!");
        else Debug.Log("HungryZombie encontró al Player, Nombre Objeto: " + player.gameObject.name);
    }

    void Update()
    {
        // usar un timer para calcular aStar cada pathRecalculateRate-segundos
        pathRecalculateTimer -= Time.deltaTime;
        if (pathRecalculateTimer <= 0)
        {
            pathRecalculateTimer = pathRecalculateRate;
            RecalculatePath();
        }
    }

    void FixedUpdate()
    {
        HandleMovement(); // logica de movimiento
    }

    private void RecalculatePath()
    {
        if (player == null || aStar == null) return;

        // calcular el camino hacia el jugador!!!
        currentPath = aStar.FindPath(transform.position, player.position);
        
        // empezar desde el primer nodo del camino
            // el primer nodo (0) suele ser el mismo en el que ya se esta
            // Apuntar al segundo nodo (1) suele dar un movimiento mas fluido
        if (currentPath != null && currentPath.Count > 0) 
            currentPathIndex = 1;
        else
            currentPathIndex = 0;
    }

    private void HandleMovement()
    {
        // Si no hay camino, o se llego al final, no moverse
        if (currentPath == null || currentPath.Count == 0 || currentPathIndex >= currentPath.Count)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // obtener el waypoint actual al que nos dirigimos
        GraphNode targetNode = currentPath[currentPathIndex];
        Vector3 targetPosition = targetNode.WorldPosition;

        // ignorar eje z
        targetPosition.z = transform.position.z;

        // moverse hacia el jugador con Seek
        Vector2 direction = (targetPosition - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        // revisar si se llego al waypoint
        // Si estamos lo suficientemente cerca, avanzamos al siguiente waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
            currentPathIndex++;
            
    }
}