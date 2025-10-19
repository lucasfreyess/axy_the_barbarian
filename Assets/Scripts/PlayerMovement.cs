using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;  // Necesario para EditorApplication.Beep()
#endif


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Vector2 moveVector; // vector de movimiento del jugador

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ProcessInput();
    }

    private void FixedUpdate()
    {
        FixedUpdateState();
    }

    private void ProcessInput()
    {
        ProcessMovementInput();
        ProcessColorChangeInput(); // lo de cambiar el color del avatar con espacio
    }

    private void ProcessMovementInput()
    {
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) y = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) y = -1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) x = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x = 1f;

        // actualizar el vector interno de direccion del movimiento (no es el transform ni el rb del jugador!!)
        moveVector = new Vector2(x, y).normalized;
    }

    private void ProcessColorChangeInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            sr.color = new Color(Random.value, Random.value, Random.value);
    }

    private void FixedUpdateState()
    {
        // mover físicamente al jugador
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * moveVector);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Enemy"))
        {
            #if UNITY_EDITOR
            EditorApplication.Beep(); // Sonido en el editor
            #endif

            if (TryGetComponent<AudioSource>(out var audio)) audio.Play();
        }
    }
}