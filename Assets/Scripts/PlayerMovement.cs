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

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcessInput();  // leer el input aquí
    }

    void FixedUpdate()
    {
        // mover físicamente al jugador (colisiones correctas)
        rb.MovePosition(rb.position + moveVector * speed * Time.fixedDeltaTime);
    }

    void ProcessInput()
    {
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) y = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) y = -1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) x = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x = 1f;

        moveVector = new Vector2(x, y).normalized;
        // Cambiar color al presionar espacio
        if (Input.GetKeyDown(KeyCode.Space))
            sr.color = new Color(Random.value, Random.value, Random.value);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Enemy"))
        {
            #if UNITY_EDITOR
            EditorApplication.Beep(); // Sonido en el editor
            #endif

            var audio = GetComponent<AudioSource>();
            if (audio != null) audio.Play();
        }
    }
}