using System.Diagnostics;
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
        // Movimiento
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) y = 2f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) y = -2f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) x = -2f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x = 2f;

        moveVector = new Vector2(x, y).normalized;

        // Cambiar color del avatar con espacio
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
