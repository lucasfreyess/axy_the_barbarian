using UnityEngine;


public class GraphNode
{
    // posicion en el grid
    public int X { get; }
    public int Y { get; }

    // posicion en el mundo real
    public Vector3 WorldPosition { get; }

    public bool IsObstacle { get; set; }

    // costo real desde el nodo inicial hasta este nodo
    public float GValue { get; set; }

    // distancia euclidiana desde este nodo hasta el nodo final
    public float HValue { get; set; }

    // nodo del que vino este nodo en el camino mas corto
    public GraphNode CameFrom { get; set; }

    public float F()
    {
        return GValue + HValue;
    }

    public void PrintNode()
    {
        Debug.Log($"GraphNode({X}, {Y}) PosReal: {WorldPosition} Obstacle: {IsObstacle}, G: {GValue}, H: {HValue}, CameFrom: {(CameFrom != null ? $"({CameFrom.X}, {CameFrom.Y})" : "null")})");
    }

    // constructor
    public GraphNode(int x, int y, Vector3 worldPosition)
    {
        this.X = x;
        this.Y = y;
        this.WorldPosition = worldPosition;
        this.IsObstacle = false;

        GValue = float.MaxValue;
        CameFrom = null;
    }
}