using System;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class PlayerPhysicsComponent : PhysicsComponent
{
    [SerializeField] private PlayerController player; // para obtener moveDirection
    [SerializeField] private PlayerAudioComponent audioComponent; // quiero cambiar esto pq esta muy tigthly coupled pero no me da el tiempo!!
    [SerializeField] private float speed = 20f;

    public event Action OnGameWon;
    public event Action OnGameLost;

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
            if (audioComponent != null) audioComponent.PlayBeepSound();
        }
        if (collidedObjectTag == "Exit" || collidedObjectTag == "Enemy" || collidedObjectTag == "Arrow")
        {
            canPlayerMove = false;

            if (collidedObjectTag == "Exit") OnGameWon?.Invoke(); // jugador gana!
            else OnGameLost?.Invoke(); // jugador muere!
        }
    }
}