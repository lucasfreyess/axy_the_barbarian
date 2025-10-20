using UnityEngine;


public class SkeletonShootingComponent : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;     // prefab para instanciar la flecha
    [SerializeField] private float arrowAreaRadius = 3f; // area alrededor del esqueleto donde puede aparecer la flecha
    private GameObject currentArrow;                     // para poder destruirla cuando se spawnee otra jejjeje

    public void ShootArrow()
    {
        if (currentArrow != null) Destroy(currentArrow);

        // se crea una flecha entre la posicion del SkeletonArcher y el area definida por arrowAreaRadius
        float arrowPositionY = Random.Range(transform.position.y - arrowAreaRadius, transform.position.y + arrowAreaRadius);
        float arrowPositionX = Random.Range(transform.position.x - arrowAreaRadius, transform.position.x + arrowAreaRadius);

        currentArrow = Instantiate(arrowPrefab, new Vector2(arrowPositionX, arrowPositionY), Quaternion.identity);
        Debug.Log("Flecha creada en: " + currentArrow.transform.position);
    }
}