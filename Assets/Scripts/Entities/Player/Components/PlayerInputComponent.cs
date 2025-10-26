using UnityEngine;


public class PlayerInputComponent : InputComponent
{
    [SerializeField] private PlayerController player;

    public override void ProcessInput()
    {
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) y = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) y = -1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) x = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x = 1f;

        // actualizar el vector interno de direccion del movimiento (no es el transform ni el rb del jugador!!)
        player.SetMoveDirection(new Vector2(x, y).normalized);
    }
}