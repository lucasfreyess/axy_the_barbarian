using UnityEngine;


// este componente funciona como el que updatea el state, mientras que InputComponent procesa el "input" (la IA)
public class GazerMovementComponent : MovementComponent
{
    //topY = 3f   //se setea en el inspector del GazerController (startingY)
    //fixedX = 2f //se setea en el inspector del GazerController (startingX)
    [SerializeField] private GazerController gazer;
    [SerializeField] private float speed = 3.65f;

    public override void Start()
    {
        // Gazer empieza en (2,3), va hacia (2,-3) y se devuelve
        transform.position = new Vector2(gazer.startingX, gazer.startingY);
    }

    public override void UpdateState()
    {
        // se actualiza el movimiento segun el booleano de direccion del gazer
        Vector2 moveDirection = gazer.GetIsGazerMovingUp() ? Vector2.up : Vector2.down;
        transform.Translate(speed * Time.deltaTime * moveDirection);
    }
}