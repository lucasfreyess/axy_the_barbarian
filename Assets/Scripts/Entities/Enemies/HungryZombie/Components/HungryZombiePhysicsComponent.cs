using System.Collections.Generic;
using UnityEngine;


public class HungryZombiePhysicsComponent : PhysicsComponent
{
    [Header("Zombie Controller")]
    [SerializeField] private HungryZombieController zombie;

    [Header("Propiedades")]
    [SerializeField] private float speed = 1.5f;


    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        HandleMovement();
    }

    private void HandleMovement()
    {
        List<GraphNode> currentPath = zombie.GetCurrentPath();
        int currentPathIndex = zombie.GetCurrentPathIndex();

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
        rb.linearVelocity = speed * direction; // Vf = v * Df

        //Debug.Log($"Moviendo a waypoint {currentPathIndex} en {targetPosition}. " +
        //$"Distancia restante: {Vector3.Distance(transform.position, targetPosition)}");

        // revisar si se llego al nodo n
        // si se esta lo suficientemente cerca, se avanza al siguiente nodo
        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
        {
            //Debug.LogWarning($"¡¡LLEGUÉ AL WAYPOINT {currentPathIndex}!! Avanzando al siguiente.");
            zombie.SetCurrentPathIndex(currentPathIndex + 1);
        }
    }
}