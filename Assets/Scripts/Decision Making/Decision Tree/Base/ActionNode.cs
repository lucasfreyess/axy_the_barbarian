using UnityEngine;


public class ActionNode : Node
{
    public override Node Decide(GameObject obj, GameObject world)
    {
        return this;
    }
}
