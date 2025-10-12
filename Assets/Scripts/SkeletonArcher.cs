using UnityEngine;

public class SkeletonArcher : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;     // prefab para generar la flecha 
    [SerializeField] private float StartingX = 8f;       // posicion fija en X del esqueleto
    [SerializeField] private float StartingY = 0f;       // posicion fija en Y del esqueleto
    [SerializeField] private float shootInterval = 3f;   // no puede ser igual que la velocidad del Gazer
    [SerializeField] private float arrowAreaRadius = 3f; // area alrededor del esqueleto donde puede aparecer la flecha

    private float shootIntervalTimer = 0f;
    private GameObject currentArrow;                     // para poder destruirla cuando se spawnee otra jejjeje

    
    void Start()
    {
        // SkeletonArcher empieza en (8,0) y no se mueve
        transform.position = new Vector2(StartingX, StartingY);
    }

    void Update()
    {
        ProcessActions(); // procesa las acciones del esqueleto (por ahora solo dispara flechas xd)
        UpdateState();
    }

    void ProcessActions()
    {
        ProcessArrowShooting();
    }

    void ProcessArrowShooting()
    {
        // cuando el timer llega a 3 segundos, se dispara una flecha
        if (shootIntervalTimer >= shootInterval)
        {
            Debug.Log("Timer excedio el intervalo!");
            ShootArrow();
            shootIntervalTimer = 0f; // reiniciar el timer
        }
    }

    void ShootArrow()
    {
        if (currentArrow != null) Destroy(currentArrow);

        // se crea una flecha entre la posicion del SkeletonArcher y el area definida por arrowAreaRadius
        float arrowPositionY = Random.Range(transform.position.y - arrowAreaRadius, transform.position.y + arrowAreaRadius);
        float arrowPositionX = Random.Range(transform.position.x - arrowAreaRadius, transform.position.x + arrowAreaRadius);

        currentArrow = Instantiate(arrowPrefab, new Vector2(arrowPositionX, arrowPositionY), Quaternion.identity);
        Debug.Log("Flecha creada en: " + currentArrow.transform.position);
    }

    void UpdateState()
    {
        // el unico estado interno (que se modifica actualmente) del esqueleto es el timer para disparar flechas!!
        shootIntervalTimer += Time.deltaTime;
    }
}
