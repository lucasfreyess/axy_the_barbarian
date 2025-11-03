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

                float costToNeighbor = 1.0f;
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
        
        if (startNode.IsObstacle) return 1;
        if (goalNode.IsObstacle) return 1;
        
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
}