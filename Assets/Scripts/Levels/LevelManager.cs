using UnityEngine;


public class LevelLoader : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject wallPrefab;
    public GameObject gazerEnemyPrefab;
    public GameObject skeletonArcherPrefab;
    public GameObject exitWallPrefab;

    [Header("Level File")]
    public string levelFileName = "Level01";

    private const float WALL_X_SIZE = 0.9412677f;
    private const float WALL_Y_SIZE = 19.27043f;

    private LevelData level;

    private void Start()
    {
        LoadLevel(levelFileName);
    }

    private void LoadLevel(string fileName)
    {
        // Se lee el json con el nombre del archivo desde Resources/Levels
        TextAsset jsonFile = Resources.Load<TextAsset>($"Levels/{fileName}");
        if (jsonFile == null)
        {
            Debug.LogError("No se encontró el archivo JSON del nivel: " + fileName);
            return;
        }

        // Se lee el JSON con las estructuras del LevelData.cs
        level = JsonUtility.FromJson<LevelData>(jsonFile.text);

        // generar todos los game objects descritos en el json
        GenerateGameObjects();
    }

    private void GenerateGameObjects()
    {
        GenerateWalls();
        GenerateEnemies();
    }

    private void GenerateWalls()
    {
        // walls normales
        foreach (var w in level.wall)
        {
            GameObjectFactory.CreateWall(wallPrefab, w.posX, w.posY, w.rotation, w.scaleY);
        }

        // wall exit
        var exit = level.exitWall;
        GameObjectFactory.CreateExitWall(exitWallPrefab, exit.posX, exit.posY, exit.rotation, exit.scaleY);
    }

    private void GenerateEnemies()
    {
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
    }
    
}
