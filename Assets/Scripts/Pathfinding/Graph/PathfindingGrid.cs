using UnityEngine;
using System.Collections.Generic;


public class PathfindingGrid : MonoBehaviour
{
    public GraphNode[,] Nodes { get; private set; }

    // tamaño del mundo
    [SerializeField] private int gridWidth = 186;
    [SerializeField] private int gridHeight = 84;

    [SerializeField] private float cellSize = 1f;

    // origen del grid (la esquina inferior izquierda)
    [SerializeField] private Vector3 gridOrigin = new(-36, -56, 0);

    // para detectar qué es un obstáculo
    [SerializeField] private LayerMask obstacleLayer;

    void Start()
    {
        GlobalListener.Instance.OnLevelsGenerated += CreateGrid;
    }

    private void CreateGrid()
    {
        Nodes = new GraphNode[gridWidth, gridHeight];

        // Crear todos los nodos
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Calcula la posición en el mundo real
                Vector3 worldPos = gridOrigin + new Vector3(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2, 0);
                Nodes[x, y] = new GraphNode(x, y, worldPos);
            }
        }

        // Marcar los obstáculos
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GraphNode node = Nodes[x, y];

                // Revisar por collider de obstáculo en la pos de este nodo
                if (Physics2D.OverlapCircle(node.WorldPosition, cellSize / 2.1f, obstacleLayer))
                {
                    //Debug.Log($"Obstaculo en nodo ({node.X}, {node.Y}) Pos Real: {node.WorldPosition}");
                    node.IsObstacle = true;
                }
            }
        }

        //PrintGrid();
    }

    public void PrintGrid()
    {
        for (int y = gridHeight - 1; y >= 0; y--)
        {
            string row = "";
            for (int x = 0; x < gridWidth; x++)
            {
                row += Nodes[x, y].IsObstacle ? "X " : "O ";
            }
            Debug.Log(row);
        }
    }

    // =================== Metodos ======================

    // obtener un nodo desde coordenadas del grid
    public GraphNode GetNode(int x, int y)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
        {
            return Nodes[x, y];
        }
        return null;
    }

    // obtener un nodo desde una posición del mundo
    public GraphNode GetNodeFromWorldPoint(Vector3 worldPosition)
    {
        // convierte la pos del mundo a coordenadas de grid
        float percentX = (worldPosition.x - gridOrigin.x) / (gridWidth * cellSize);
        float percentY = (worldPosition.y - gridOrigin.y) / (gridHeight * cellSize);
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.FloorToInt(gridWidth * percentX);
        int y = Mathf.FloorToInt(gridHeight * percentY);

        // Clamp para evitar errores en los bordes
        x = Mathf.Clamp(x, 0, gridWidth - 1);
        y = Mathf.Clamp(y, 0, gridHeight - 1);

        return Nodes[x, y];
    }

    // obtener los vecinos
    public List<GraphNode> GetNeighbors(GraphNode node)
    {
        List<GraphNode> neighbors = new List<GraphNode>();

        // revisar 8 vecinos (incluyendo diagonales)
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // ignorar nodo actual
                if (x == 0 && y == 0) continue;

                int checkX = node.X + x;
                int checkY = node.Y + y;

                GraphNode neighborNode = GetNode(checkX, checkY);

                // si el vecino existe y no es un obstáculo, se agrega a la lista
                if (neighborNode != null && !neighborNode.IsObstacle)
                {
                    neighbors.Add(neighborNode);
                }
            }
        }

        return neighbors;
    }
}
