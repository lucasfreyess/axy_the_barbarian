using UnityEngine;

public static class GameObjectFactory
{
    private const float WALL_X_SIZE = 0.9412677f;
    private const float WALL_Y_SIZE = 20f;

    // Crear muros
    public static GameObject CreateWall(GameObject prefab, float x, float y, float rotation, float scaleY)
    {
        GameObject wall = Object.Instantiate(prefab, new Vector2(x, y), Quaternion.Euler(0, 0, rotation));
        Vector3 scale = wall.transform.localScale;
        scale.x = WALL_X_SIZE;
        scale.y = WALL_Y_SIZE * scaleY;
        wall.transform.localScale = scale;
        return wall;
    }

    // Crear enemigos
    public static GameObject CreateEnemy(GameObject prefab, float startingX, float startingY)
    {
        GameObject enemy = Object.Instantiate(prefab);
        var entity = enemy.GetComponent<GameEntity>();
        if (entity != null)
        {
            entity.startingX = startingX;
            entity.startingY = startingY;
            enemy.transform.position = new Vector2(startingX, startingY);
        }
        return enemy;
    }

    // Crear EndWall
    public static GameObject CreateEndWall(GameObject prefab, float x, float y, float rotation, float scaleY)
    {
        GameObject wall = Object.Instantiate(prefab, new Vector2(x, y), Quaternion.Euler(0, 0, rotation));
        Vector3 scale = wall.transform.localScale;
        scale.x = WALL_X_SIZE;
        scale.y = WALL_Y_SIZE * scaleY;
        wall.transform.localScale = scale;
        return wall;
    }
}
