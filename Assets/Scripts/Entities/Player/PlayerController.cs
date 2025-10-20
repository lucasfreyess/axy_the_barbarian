using System;
using UnityEngine;


public class PlayerController : GameEntity
{    
    private Vector2 moveDirection = Vector2.zero; // vector de direccion de movimiento del jugador

    protected override void Start()
    {
        base.Start();
        // PlayerPhysicsComponent se encarga de inicializar el RigidBody, y PlayerGraphicsComponent se encarga del SpriteRenderer
    }

    protected override void Update()
    {
        base.Update();
        // no existe otra logica de update-eo aparte de:
            // procesar Input, 
            // mover al personaje (Movement y Physics) 
            // y actualizar el sprite cuando se apreta espacio
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate(); // physics update
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

    public void SetMoveDirection(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection;
    }
}