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
        if (isGoalAnObstacle == 1) return null;

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
            // (Lo ponemos aquí para que no volvamos a procesar este nodo)
            visited.Add(currentNode);

            // ==== For every neighbor s’ of s that is not in VISITED ====
            foreach (GraphNode neighbor in grid.GetNeighbors(currentNode))
            {
                // ignorar vecino si ya esta en VISITED
                if (visited.Contains(neighbor)) continue;

                // distancia euclidiana tambien es usada para calcular G (lo cual probablemente esta mal)
                float costToNeighbor = GetHeuristic(currentNode, neighbor);
                float tentativeGScore = currentNode.GValue + costToNeighbor;

                // ==== If g(s’ ) > g(s) + cost: ====
                if (tentativeGScore < neighbor.GValue)
                {
                    // se encontro un camino mejor hacia este vecino
                    neighbor.CameFrom = currentNode; // s’.cameFrom = s
                    neighbor.GValue = tentativeGScore; // g(s’) = g(s) + cost
                    neighbor.HValue = GetHeuristic(neighbor, goalNode); // h(s')

                    // ==== Insert (s’ ) in TO_VISIT ====
                    if (!toVisit.Contains(neighbor))
                        toVisit.Add(neighbor);
                }
            }
        }

        // si el loop termina, TO_VISIT esto vacio y no se encontro el goalNode
        return null;
    }

    private int InitAStar(Vector3 startWorldPos, Vector3 endWorldPos)
    {
        goalNode = grid.GetNodeFromWorldPoint(endWorldPos);

        if (goalNode.IsObstacle) return 1;

        // inicializar TO_VISIT y VISITED
        //toVisit = new List<GraphNode>();
        //visited = new HashSet<GraphNode>(); // HashSet es más rápido para buscar

        // reiniciar todos los nodos del grid
        // si no se hace esto, los cálculos de un frame anterior afectarán al cálculo de este frame.
        foreach (var node in grid.Nodes)
        {
            node.GValue = float.MaxValue;
            node.HValue = 0;
            node.CameFrom = null;
        }

        // configuracion del nodo inicial
        GraphNode startNode = grid.GetNodeFromWorldPoint(startWorldPos);
        startNode.GValue = 0;
        startNode.HValue = GetHeuristic(startNode, goalNode); // h(n)

        // startNode es el unico nodo en TO_VISIT inicialmente
        toVisit.Add(startNode);

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

        // el camino esta desde goal a start, así que lo invertimos
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