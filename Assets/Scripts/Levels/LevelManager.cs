using UnityEngine;
using System.IO;

public class LevelLoader : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject wallPrefab;
    public GameObject gazerEnemyPrefab;
    public GameObject skeletonArcherPrefab;
    public GameObject endWallPrefab;

    [Header("Level File")]
    public string levelFileName = "Level01";

    private const float WALL_X_SIZE = 0.9412677f;
    private const float WALL_Y_SIZE = 19.27043f;

    void Start()
    {
        LoadLevel(levelFileName);
    }

    void LoadLevel(string fileName)
    {
        // Se lee el json con el nombre del archivo desde Resources/Levels
        TextAsset jsonFile = Resources.Load<TextAsset>($"Levels/{fileName}");
        if (jsonFile == null)
        {
            Debug.LogError("No se encontró el archivo JSON del nivel: " + fileName);
            return;
        }

        // Se lee el JSON con las estructuras del LevelData.cs
        LevelData level = JsonUtility.FromJson<LevelData>(jsonFile.text);

        // Walls
        foreach (var w in level.wall)
        {
            GameObjectFactory.CreateWall(wallPrefab, w.posX, w.posY, w.rotation, w.scaleY);
        }

        // Enemigos
        foreach (var enemy in level.enemies)
        {
            GameObject prefab = null;
            if (enemy.type == "GazerEnemy") prefab = gazerEnemyPrefab;
            else if (enemy.type == "SkeletonArcherEnemy") prefab = skeletonArcherPrefab;

            if (prefab != null)
            {
                GameObjectFactory.CreateEnemy(prefab, enemy.startingX, enemy.startingY);
            }
        }

        // EndWall
        var exit = level.endWall;
        GameObjectFactory.CreateEndWall(endWallPrefab, exit.posX, exit.posY, exit.rotation, exit.scaleY);
        }
}
