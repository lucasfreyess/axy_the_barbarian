using System.Collections.Generic;
using UnityEngine;


public class LevelLoader : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject gazerEnemyPrefab;
    public GameObject skeletonArcherEnemyPrefab;
    [SerializeField] private GameObject cowardRatEnemyPrefab;
    [SerializeField] private GameObject hungryZombieEnemyPrefab;
    
    [Header("Wall Prefabs")]
    public GameObject wallPrefab;
    public GameObject exitWallPrefab;

    [Header("Level Stuff")]
    public string levelFileName = "Level01";
    public bool loadAllLevels = true; // si se pone true, se loadean todos los json dentro de Assets/Resources/Levels

    //private const float WALL_X_SIZE = 0.9412677f;
    //private const float WALL_Y_SIZE = 19.27043f;

    private LevelData level;
    private Dictionary<string, GameObject> enemyDictionary;

    private void Start()
    {
        // inicializar diccionario de enemigos para fetchear facilmente en GenerateEnemies()
        InitEnemyDictionary();

        // cargar niveles
        if (loadAllLevels) LoadAllLevels();
        else LoadLevel(levelFileName);

        GlobalListener.Instance.NotifyLevelsGenerated();
    }

    private void InitEnemyDictionary()
    {
        enemyDictionary = new Dictionary<string, GameObject>
        {
            { "GazerEnemy", gazerEnemyPrefab },
            { "SkeletonArcherEnemy", skeletonArcherEnemyPrefab },
            { "CowardRatEnemy", cowardRatEnemyPrefab },
            { "HungryZombieEnemy", hungryZombieEnemyPrefab }
        };
    }
    
    private void LoadAllLevels()
    {
        var assets = Resources.LoadAll<TextAsset>("Levels");
        if (assets == null || assets.Length == 0)
        {
            Debug.LogWarning("Niveles no encontrados en Assets/Resources/Levels/");
            return;
        }

        // añadir niveles a lista si empiezan con Level y le sigue un numero de dos digitos (e.g., 01, 11, entre otros)
        var levelNames = new List<string>();
        foreach (var a in assets)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(a.name, @"^Level\d{2}$"))
                levelNames.Add(a.name);
        }

        if (levelNames.Count == 0)
        {
            Debug.LogWarning("Niveles no encontrados en Assets/Resources/Levels/");
            return;
        }

        // cargar niveles
        foreach (var name in levelNames)
        {
            Debug.Log("Cargando nivel: " + name);
            LoadLevel(name);
        }
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
        if (enemyDictionary == null)
        {
            Debug.LogError("Diccionario de enemigos no fue inicializado!!!!");
            return;
        }

        foreach (var enemy in level.enemies)
        {
            GameObject enemyPrefab = enemyDictionary[enemy.type];

            if (enemyPrefab == null) return;
            
            GameObjectFactory.CreateEnemy(enemyPrefab, enemy.startingX, enemy.startingY);
        }
    }
}
