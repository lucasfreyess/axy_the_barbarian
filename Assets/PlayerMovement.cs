using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20f;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Movimiento
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) y = 2f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) y = -2f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) x = -2f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x = 2f;

        Vector2 move = new Vector2(x, y).normalized;
        transform.Translate(move * speed * Time.deltaTime);

        // Cambiar color del avatar con espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sr.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}
