using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsComponent : MonoBehaviour
{
    public Rigidbody2D rb;

    public virtual void FixedUpdateState() {}
}
