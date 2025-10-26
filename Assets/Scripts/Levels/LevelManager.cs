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

        // Se lee el JSON
        LevelData level = JsonUtility.FromJson<LevelData>(jsonFile.text);

        // Walls
        foreach (var w in level.wall)
        {
            GameObject wallObj = Instantiate(wallPrefab, new Vector2(w.posX, w.posY), Quaternion.Euler(0, 0, w.rotation));
            Vector3 scale = wallObj.transform.localScale;
            scale.x = WALL_X_SIZE;
            scale.y = WALL_Y_SIZE * w.scaleY;
            wallObj.transform.localScale = scale;
        }

        // Enemigos
        foreach (var enemy in level.enemies)
        {
            GameObject enemyPrefab = null;
            if (enemy.type == "GazerEnemy") enemyPrefab = gazerEnemyPrefab;
            else if (enemy.type == "SkeletonArcherEnemy") enemyPrefab = skeletonArcherPrefab;

            if (enemyPrefab != null)
            {
                GameObject enemyObj = Instantiate(enemyPrefab);
                var entity = enemyObj.GetComponent<GameEntity>();
                entity.startingX = enemy.startingX;
                entity.startingY = enemy.startingY;
                enemyObj.transform.position = new Vector2(entity.startingX, entity.startingY);
            }
        }

        // EndWall
        var exit = level.endWall;
        GameObject exitObj = Instantiate(endWallPrefab, new Vector2(exit.posX, exit.posY), Quaternion.Euler(0, 0, exit.rotation));
        Vector3 exitScale = exitObj.transform.localScale;
        exitScale.x = WALL_X_SIZE;
        exitScale.y = WALL_Y_SIZE * exit.scaleY;
        exitObj.transform.localScale = exitScale;
    }
}
