using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private SpriteRenderer sr;
    private Vector2 moveVector; // vector de movimiento del jugador

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ProcessInput();
        UpdateState();
    }

    void ProcessInput()
    {
        ProcessMovementInput();
        ProcessColorChangeInput(); // lo de cambiar el color del avatar con espacio
    }

    void ProcessMovementInput()
    {
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) y = 2f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) y = -2f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) x = -2f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x = 2f;

        // actualizar el vector interno de direccion del movimiento (no es el transform del jugador!!)
        moveVector = new Vector2(x, y).normalized;
    }

    void ProcessColorChangeInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sr.color = new Color(Random.value, Random.value, Random.value);
        }
    }
    
    void UpdateState()
    {
        // mover al jugador
        transform.Translate(speed * Time.deltaTime * moveVector);
    }
}
