using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class HungryZombieController : GameEntity
{
    [Header("Pathfinding")]
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float pathRecalculateRate = 1f; // para recalcular el camino 1 vez por segundo

    [Header("Propiedades")]
    [SerializeField] private Rigidbody2D rb;

    private Transform player;
    private float pathRecalculateTimer;
    private AStarAlgorithm aStar;
    
    // el camino que se sigue actualmente
    private List<GraphNode> currentPath;
    private int currentPathIndex;

    protected override void Start()
    {
        base.Start();

        // encontrar al jugador
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) player = playerObject.transform;
        
        // encontrar el script de aStar dentro de PathfindingObject
        GameObject pathfindingObject = GameObject.Find("PathfindingObject");
        if (pathfindingObject != null)
            aStar = pathfindingObject.GetComponent<AStarAlgorithm>();

        if (aStar == null) Debug.LogError("HungryZombie no pudo encontrar el AStarAlgorithm!");
        else Debug.Log("HungryZombie encontro el AStarAlgorithm, Nombre Objeto: " + aStar.gameObject.name);

        if (player == null) Debug.LogError("HungryZombie no pudo encontrar al Player!");
        else Debug.Log("HungryZombie encontro al Player, Nombre Objeto: " + player.gameObject.name);
    }

    protected override void Update()
    {
        // usar un timer para calcular aStar cada pathRecalculateRate-segundos
        pathRecalculateTimer -= Time.deltaTime;
        if (pathRecalculateTimer <= 0)
        {
            pathRecalculateTimer = pathRecalculateRate;
            RecalculatePath();
        }
    }

    protected override void FixedUpdate()
    {
        HandleMovement(); // logica de movimiento
    }

    private void RecalculatePath()
    {
        if (player == null || aStar == null) return;

        // calcular el camino hacia el jugador!!!
        currentPath = aStar.FindPath(transform.position, player.position);

        if (currentPath != null && currentPath.Count > 1) // Entonces, hay un camino con mas de 1 nodo.
        {
            
            // se apunta al segundo nodo del camino (indice 1), porque el primero (indice 0) es donde ya esta el zombie.
            currentPathIndex = 1;
            //Debug.Log($"HungryZombie obtuvo un camino con {currentPath.Count} nodos. Apuntando al indice 1.");
        }
        else if (currentPath != null && currentPath.Count == 1) // El camino solo tiene 1 nodo
        {
            // ya se esta en el nodo final, por lo que se apunta al 0 (zombie) y no se sigue moviendo.
            currentPathIndex = 0;
            //Debug.Log("HungryZombie obtuvo camino de 1 nodo (se llego al destino).");
        }
        else // no se encontro un camino.
        {
            currentPath = new List<GraphNode>();
            currentPathIndex = 0;
            //Debug.Log("HungryZombie obtuvo un camino con 0 nodos.");
        }
    }

    private void HandleMovement()
    {
        // Si no hay camino, o se llego al final, no moverse
        if (currentPath == null || currentPath.Count == 0 || currentPathIndex >= currentPath.Count)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // Si el camino solo tiene 1 nodo y ya se esta en el, no moverse
        if (currentPath.Count == 1 && currentPathIndex == 0)
        {
            // chequear distancia porsiacaso
            if (Vector3.Distance(transform.position, currentPath[0].WorldPosition) < 0.3f)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }
        }

        // obtener nodo n al que se dirige el zombie
        GraphNode targetNode = currentPath[currentPathIndex];
        Vector3 targetPosition = targetNode.WorldPosition;

        // ignorar eje z
        targetPosition.z = transform.position.z;

        // moverse hacia el jugador con Seek
        Vector2 direction = (targetPosition - transform.position).normalized;
        rb.linearVelocity = direction * speed;

        // revisar si se llego al nodo n
            // si se esta lo suficientemente cerca, se avanza al siguiente nodo
        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
            currentPathIndex++;
    }
}