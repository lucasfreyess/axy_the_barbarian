using System;


// Estructuras de datos para leer el JSON
// Para construir niveles dínamicamente
[Serializable]
public class LevelData
{
    public string levelName;
    public WallData[] wall;
    public EnemyData[] enemies;
    public ExitWallData exitWall;
}

[Serializable]
public class WallData
{
    public float posX;
    public float posY;
    public float rotation;
    public float scaleY;
}

[Serializable]
public class EnemyData
{
    public string type;
    public float startingX;
    public float startingY;
}

[Serializable]
public class ExitWallData
{
    public float posX;
    public float posY;
    public float rotation;
    public float scaleY;
}
