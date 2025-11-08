using UnityEngine;


public class DayNightCycle : MonoBehaviour
{
    [Header("Duracion del ciclo")]
    public float cycleDurationInSeconds = 10f; // incluye dia y noche

    [Header("Colores del Background")]
    public Color dayColor = Color.white;
    public Color nightColor = Color.black;

    [HideInInspector] public bool isItNight = false;

    private Camera mainCamera;
    private float currentTime = 0f;
    private float dayPercentage = 0f; // 0=dia, 1=noche

    void Start()
    {
        mainCamera = Camera.main;
        
        // color inicial (del dia)
        mainCamera.backgroundColor = dayColor;
    }

    void Update()
    {
        // updatear tiempo
        currentTime += Time.deltaTime;
        if (currentTime >= cycleDurationInSeconds) currentTime = 0f;
        
        // de 0 a 1: de 0 a 0.5 es de dia; de 0.5 a 1 es de noche
        dayPercentage = currentTime / cycleDurationInSeconds;
        isItNight = dayPercentage >= 0.5f;

        if (isItNight)
            mainCamera.backgroundColor = nightColor;
        else
            mainCamera.backgroundColor = dayColor;
    }
}
