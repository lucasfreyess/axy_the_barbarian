using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class PlayerPhysicsComponent : PhysicsComponent
{
    [SerializeField] private PlayerController player; // para obtener moveDirection
    [SerializeField] private float speed = 20f;

    private bool canPlayerMove = true;
    
    public override void FixedUpdateState()
    {
        if (!canPlayerMove) return;

        // mover físicamente al jugador
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * player.GetMoveDirection());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collidedObjectTag = collision.collider.tag;

        if (collidedObjectTag == "Wall" || collidedObjectTag == "Exit" || collidedObjectTag == "Enemy" || collidedObjectTag == "Arrow")
        {
            GlobalListener.Instance.NotifyPlayerCollision();
        }
        if (collidedObjectTag == "Exit" || collidedObjectTag == "Enemy" || collidedObjectTag == "Arrow")
        {
            canPlayerMove = false;

            if (collidedObjectTag == "Exit") GlobalListener.Instance.NotifyGameWon(); // jugador gana!
            else GlobalListener.Instance.NotifyGameLost(); // jugador muere!
        }
    }
}