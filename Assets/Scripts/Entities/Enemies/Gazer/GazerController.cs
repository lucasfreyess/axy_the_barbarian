using UnityEngine;


public class GazerController : MonoBehaviour
{
    [SerializeField] private float speed = 3.65f;
    [SerializeField] private float topY = 3f;
    [SerializeField] private float bottomY = -3f;
    [SerializeField] private float fixedX = 2f;

    private bool isGazerMovingUp = false;

    void Start()
    {
        // Gazer empieza en (2,3), va hacia (2,-3) y se devuelve
        transform.position = new Vector2(fixedX, topY);
    }

    void Update()
    {
        ProcessActions(); // procesa las acciones del Gazer (por ahora solo se mueve)
        UpdateState();
    }

    void ProcessActions()
    {
        ProcessMovement();
    }

    void ProcessMovement()
    {
        // si el gazer se esta moviendo hacia arriba y llego al tope superior
        if (isGazerMovingUp && transform.position.y >= topY) isGazerMovingUp = false;

        // si el gazer se esta moviendo hacia abajo y llego al tope inferior
        else if (!isGazerMovingUp && transform.position.y <= bottomY) isGazerMovingUp = true;
    }

    void UpdateState()
    {
        // se actualiza el movimiento segun el booleano de direccion del gazer
        Vector2 moveDirection = isGazerMovingUp ? Vector2.up : Vector2.down;
        transform.Translate(speed * Time.deltaTime * moveDirection);
    }
}