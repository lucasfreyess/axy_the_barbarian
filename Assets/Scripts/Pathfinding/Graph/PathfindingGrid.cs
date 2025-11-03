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

    // para detectar si un GameObject es un obstaculo
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Vector2 agentSize = new Vector2(0.16f, 0.16f);

    void Start()
    {
        GlobalListener.Instance.OnLevelsGenerated += CreateGrid;
    }

    private void CreateGrid()
    {
        Nodes = new GraphNode[gridWidth, gridHeight];

        // crear todos los nodos
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {   
                // calculo de posicion real
                Vector3 worldPos = gridOrigin + new Vector3(x * cellSize, y * cellSize, 0);
                Nodes[x, y] = new GraphNode(x, y, worldPos);
            }
        }

        // marcar los obstaculos
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GraphNode node = Nodes[x, y];

                // Revisar si hay collider de obstaculo en la pos de este nodo
                //if (Physics2D.OverlapCircle(node.WorldPosition, agentRadius, obstacleLayer))
                //if (Physics2D.OverlapPoint(node.WorldPosition, obstacleLayer))
                if (Physics2D.OverlapBox(node.WorldPosition, agentSize * 20f, 0f, obstacleLayer)) // 20f es necesario para que no se quede atascado en paredes
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

    // obtener un nodo desde una posicion del mundo
    public GraphNode GetNodeFromWorldPoint(Vector3 worldPosition)
    {
        // convertir la pos del mundo a coordenadas de grid
        float localX = worldPosition.x - gridOrigin.x;
        float localY = worldPosition.y - gridOrigin.y;

        int x = Mathf.RoundToInt(localX / cellSize);
        int y = Mathf.RoundToInt(localY / cellSize);

        // Clamp para evitar errores en los bordes
        x = Mathf.Clamp(x, 0, gridWidth - 1);
        y = Mathf.Clamp(y, 0, gridHeight - 1);

        return Nodes[x, y];
    }

    // obtener todos los vecinos, sin importar si son obstaculos.
    public List<GraphNode> GetAllNeighbors(GraphNode node)
    {
        List<GraphNode> neighbors = new List<GraphNode>();

        // Revisa 8 vecinos
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue; // es el nodo central, ignorar
                    
                int checkX = node.X + x;
                int checkY = node.Y + y;

                GraphNode neighborNode = GetNode(checkX, checkY);
                
                // Si el vecino existe (esta dentro del grid), se añade a la lista de neighbors
                if (neighborNode != null) neighbors.Add(neighborNode);
            }
        }
        return neighbors;
    }

    // filtra GetAllNeighbors para obtener solo aquellos que no son obstaculos
    public List<GraphNode> GetNeighbors(GraphNode node)
    {
        List<GraphNode> allNeighbors = GetAllNeighbors(node);
        List<GraphNode> transitableNeighbors = new List<GraphNode>();

        foreach (var neighbor in allNeighbors)
        {
            if (!neighbor.IsObstacle)
                transitableNeighbors.Add(neighbor);
        }
        return transitableNeighbors;
    }

    // metodo para dibujar el grid en el Scene view para ver si se agarraron bien las walls!!
    void OnDrawGizmos()
    {
        if (Nodes == null) return;
        
        foreach (GraphNode node in Nodes)
        {
            // se colorea rojo si es obstaculo, o blanco si es transitable
            Gizmos.color = node.IsObstacle ? Color.red : Color.white;
            
            // Dibuja un pequeño cubo en la posicion de cada nodo
            Gizmos.DrawCube(node.WorldPosition, Vector3.one * (cellSize * 0.8f));
        }
    }
}
