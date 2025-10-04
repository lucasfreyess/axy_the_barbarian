using UnityEngine;

public class GazerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

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
        if (isGazerMovingUp)
        {
            transform.Translate(speed * Time.deltaTime * Vector2.up);   // se mueve hacia arriba

            // si llego al tope superior
            if (transform.position.y >= topY)
                isGazerMovingUp = false;
        }
        else
        {
            transform.Translate(speed * Time.deltaTime * Vector2.down); // se mueve hacia abajo

            // si llego al tope inferior
            if (transform.position.y <= bottomY)
                isGazerMovingUp = true;
        }
    }
}