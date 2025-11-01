using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsComponent : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;

    public virtual void FixedUpdateState() {}
}
