using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class HungryZombieController : GameEntity
{
    // el camino que se sigue actualmente
    private List<GraphNode> currentPath;
    private int currentPathIndex;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    // getters y setters
    public List<GraphNode> GetCurrentPath()
    {
        return currentPath;
    }

    public void SetCurrentPath(List<GraphNode> path)
    {
        currentPath = path;
    }

    public int GetCurrentPathIndex()
    {
        return currentPathIndex;
    }

    public void SetCurrentPathIndex(int pathIndex)
    {
        currentPathIndex = pathIndex;
    }
}
