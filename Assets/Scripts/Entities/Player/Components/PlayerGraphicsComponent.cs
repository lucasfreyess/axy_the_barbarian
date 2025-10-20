using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class PlayerGraphicsComponent : GraphicsComponent
{
    [SerializeField] private SpriteRenderer sr;

    public override void UpdateGraphics()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sr.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}
