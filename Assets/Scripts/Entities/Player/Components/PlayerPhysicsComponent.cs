using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class PlayerPhysicsComponent : PhysicsComponent
{
    [SerializeField] private PlayerController player; // para obtener moveDirection
    [SerializeField] private float speed = 20f;
    [SerializeField] private PlayerAudioComponent audioComponent; // quiero cambiar esto pq esta muy tigthly coupled pero no me da el tiempo!!

    public override void FixedUpdateState()
    {
        // mover físicamente al jugador
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * player.GetMoveDirection());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Enemy"))
        {
            if (audioComponent != null)
                audioComponent.PlayBeepSound();
        }
    }
}