using System.Collections.Generic;
using UnityEngine;


public class AStarAlgorithm : MonoBehaviour
{
    [SerializeField] private PathfindingGrid grid;

    private List<GraphNode> toVisit = new List<GraphNode>();
    private HashSet<GraphNode> visited = new HashSet<GraphNode>();
    private GraphNode goalNode;

    // funcion principal que llamaran enemigos que hacen pathfinding
    public List<GraphNode> FindPath(Vector3 startWorldPos, Vector3 endWorldPos)
    {
        int isGoalAnObstacle = InitAStar(startWorldPos, endWorldPos);
        if (isGoalAnObstacle == 1)
        {
            Debug.Log("Goal o Start esta en/es un obstaculo!!!");
            return null;
        }

        // comienzo del algoritmo (comente los equivalentes en pseudocodigo para no perderme jejeje)
        // ==== While(TO_VISIT not empty) ====
        while (toVisit.Count > 0)
        {
            // ==== Remove node s with smallest f(n) from TO_VISIT ====
            GraphNode currentNode = GetLowestFNode(toVisit);
            toVisit.Remove(currentNode);

            // ==== If s is goal: buildPath() and end ====
            if (currentNode == goalNode) return BuildPath(goalNode);

            // ==== Insert node s into VISITED ====
            visited.Add(currentNode);

            // ==== For every neighbor s’ of s that is not in VISITED ====
            foreach (GraphNode neighbor in grid.GetNeighbors(currentNode))
            {
                // ignorar vecino si ya esta en VISITED
                if (visited.Contains(neighbor)) continue;

                //float costToNeighbor = 1.0f;
                float costToNeighbor = GetHeuristic(currentNode, neighbor);
                float tentativeGScore = currentNode.GValue + costToNeighbor;

                // ==== If g(s’ ) > g(s) + cost: ====
                if (tentativeGScore < neighbor.GValue)
                {
                    // se encontro un camino mejor hacia este vecino
                    neighbor.CameFrom = currentNode;                    // s’.cameFrom = s
                    neighbor.GValue = tentativeGScore;                  // g(s’) = g(s) + cost
                    neighbor.HValue = GetHeuristic(neighbor, goalNode); // h(s')

                    // ==== Insert (s’ ) in TO_VISIT ====
                    if (!toVisit.Contains(neighbor))
                    {
                        //Debug.Log("Vecino agregado a TO_VISIT:");
                        //neighbor.PrintNode();
                        toVisit.Add(neighbor);
                    }
                }
            }
        }

        // si el loop termina, TO_VISIT esta vacio y no se encontro el goalNode
        return null;
    }

    private int InitAStar(Vector3 startWorldPos, Vector3 endWorldPos)
    {
        GraphNode startNode = grid.GetNodeFromWorldPoint(startWorldPos);
        goalNode = grid.GetNodeFromWorldPoint(endWorldPos);

        // si startNode es un obstaculo (e.g., el zombie se atasco)
        if (startNode.IsObstacle)
        {
            // Buscar el vecino transitable mas cercano
            GraphNode closestValidStart = FindClosestTransitableToNode(startNode, startWorldPos);
            if (closestValidStart == null) return 1;
            startNode = closestValidStart;
        }

        // si endNode es un obstaculo (e.g., el jugador esta en un muro)
        if (goalNode.IsObstacle)
        {
            GraphNode closestValidGoal = FindClosestTransitableToNode(goalNode, endWorldPos);
            if (closestValidGoal == null) return 1; // entonces jugador esta atrapado
            goalNode = closestValidGoal;
        }

        // reiniciar listas y todos los nodos del grid
        // si no se hace esto, los calculos de un frame anterior afectaran al calculo de este frame.
        toVisit.Clear();
        visited.Clear();

        foreach (var node in grid.Nodes)
        {
            node.GValue = float.MaxValue;
            node.HValue = 0;
            node.CameFrom = null;
        }

        // configuracion del nodo inicial
        startNode.GValue = 0;
        startNode.HValue = GetHeuristic(startNode, goalNode); // h(n)
        //Debug.Log("AStarAlgorithm: Nodo inicial:");
        //startNode.PrintNode();
    
        // startNode es el unico nodo en TO_VISIT inicialmente
        toVisit.Add(startNode);
        
        //Debug.Log($"INIT: Reinicio completo. toVisit.Count: {toVisit.Count} | visited.Count: {visited.Count}");
        //Debug.Log($"INIT: Nodo inicial {startNode.X},{startNode.Y} G={startNode.GValue} H={startNode.HValue}");

        return 0;
    }

    // reconstruye el camino yendo hacia atras desde el nodo final
    private List<GraphNode> BuildPath(GraphNode goalNode)
    {
        List<GraphNode> path = new List<GraphNode>();
        GraphNode currentNode = goalNode;

        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode = currentNode.CameFrom;
        }

        // el camino esta desde goal a start, asi que se invierte
        path.Reverse();
        return path;
    }

    private float GetHeuristic(GraphNode a, GraphNode b)
    {
        // distancia euclideana
        return Vector3.Distance(a.WorldPosition, b.WorldPosition);
    }

    // busca en TO_VISIT el nodo con el valor F mas bajo
    private GraphNode GetLowestFNode(List<GraphNode> nodeList)
    {
        GraphNode lowestFNode = nodeList[0];
        for (int i = 1; i < nodeList.Count; i++)
        {
            if (nodeList[i].F() < lowestFNode.F())
            {
                lowestFNode = nodeList[i];
            }
        }
        return lowestFNode;
    }

    // metodo de utilidad para encontrar el vecino transitable mas cercano a una "muralla"
    // ya que una muralla se extiende dos nodos "radialmente", entonces existen nodos en los que no hay muralla que son marcados de la manera opuesta
    // entonces es necesario saber si el jugador o el enemigo se encuentra en alguno de dichos nodos, y retornar la celda transitable mas cercana
    private GraphNode FindClosestTransitableToNode(GraphNode referenceNode, Vector3 referenceNodeWorldPos)
    {
        GraphNode closestValidStart = null;
        float minDistance = float.MaxValue;
        foreach (var neighbor in grid.GetAllNeighbors(referenceNode)) // referenceNode tiende a ser start o goalNode
        {
            if (!neighbor.IsObstacle)
            {
                float distance = Vector3.Distance(neighbor.WorldPosition, referenceNodeWorldPos);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestValidStart = neighbor;
                }
            }
        }

        if (closestValidStart == null) return null; // 100% Atascado (sin vecinos)
        return closestValidStart;
    }
}
