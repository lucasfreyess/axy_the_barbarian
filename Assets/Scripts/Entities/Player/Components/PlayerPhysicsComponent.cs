using System;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class PlayerPhysicsComponent : PhysicsComponent
{
    [SerializeField] private PlayerController player; // para obtener moveDirection
    [SerializeField] private float speed = 20f;

    public event Action OnCollision;  //para que el soundcomponent pueda reproducir sonido!
    public event Action OnGameWon;     //para imprimir texto de victoria/derrota en UI/TextWriter.cs
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
            OnCollision?.Invoke();
        }
        if (collidedObjectTag == "Exit" || collidedObjectTag == "Enemy" || collidedObjectTag == "Arrow")
        {
            canPlayerMove = false;

            if (collidedObjectTag == "Exit") OnGameWon?.Invoke(); // jugador gana!
            else OnGameLost?.Invoke(); // jugador muere!
        }
    }
}