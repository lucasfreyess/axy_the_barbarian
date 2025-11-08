using System.Collections.Generic;
using UnityEngine;


// funge como el componente de IA del zombie
public class HungryZombieInputComponent : InputComponent
{
    [Header("Zombie Controller")]
    [SerializeField] private HungryZombieController zombie;

    [Header("Pathfinding")]
    [SerializeField] private float pathRecalculateRate = 1f; // para recalcular el camino 1 vez por segundo

    private float pathRecalculateTimer;
    private Transform playerTransform;
    private AStarAlgorithm aStar;

    public override void Start()
    {
        base.Start();

        // encontrar al jugador
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) playerTransform = playerObject.transform;

        // encontrar el script de aStar dentro de PathfindingObject
        GameObject pathfindingObject = GameObject.Find("PathfindingObject");
        if (pathfindingObject != null)
            aStar = pathfindingObject.GetComponent<AStarAlgorithm>();

        if (aStar == null) Debug.LogError("HungryZombie no pudo encontrar el AStarAlgorithm!");
        else Debug.Log("HungryZombie encontro el AStarAlgorithm, Nombre Objeto: " + aStar.gameObject.name);

        if (playerTransform == null) Debug.LogError("HungryZombie no pudo encontrar al Player!");
        else Debug.Log("HungryZombie encontro al Player, Nombre Objeto: " + playerTransform.gameObject.name);
    }

    public override void ProcessInput()
    {
        base.ProcessInput();
        ProcessPathRecalculation();
    }

    private void ProcessPathRecalculation()
    {
        // usar un timer para calcular aStar cada pathRecalculateRate-segundos
        pathRecalculateTimer -= Time.deltaTime;
        if (pathRecalculateTimer <= 0)
        {
            pathRecalculateTimer = pathRecalculateRate;
            RecalculatePath();
        }
    }

    private void RecalculatePath()
    {
        if (playerTransform == null || aStar == null) return;

        // calcular el camino hacia el jugador!!!
        List<GraphNode> currentPath = aStar.FindPath(transform.position, playerTransform.position);
        zombie.SetCurrentPath(currentPath);

        if (currentPath != null && currentPath.Count > 1) // Entonces, hay un camino con mas de 1 nodo.
        {
            // se apunta al segundo nodo del camino (indice 1), porque el primero (indice 0) es donde ya esta el zombie.
            zombie.SetCurrentPathIndex(1);
            Debug.Log($"HungryZombie obtuvo un camino con {currentPath.Count} nodos. Apuntando al indice 1.");
        }
        else if (currentPath != null && currentPath.Count == 1) // El camino solo tiene 1 nodo
        {
            // ya se esta en el nodo final, por lo que se apunta al 0 (zombie) y no se sigue moviendo.
            zombie.SetCurrentPathIndex(0);
            Debug.Log("HungryZombie obtuvo camino de 1 nodo (se llego al destino).");
        }
        else // no se encontro un camino.
        {
            currentPath = new List<GraphNode>();
            zombie.SetCurrentPath(currentPath);
            zombie.SetCurrentPathIndex(0);
            Debug.Log("HungryZombie obtuvo un camino con 0 nodos.");
        }
    }
}