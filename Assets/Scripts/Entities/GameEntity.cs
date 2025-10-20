using UnityEngine;


//usado para representar al player y todos los enemigos!!
public class GameEntity : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] protected InputComponent input_;
    [SerializeField] protected MovementComponent movement_;
    [SerializeField] protected PhysicsComponent physics_;
    [SerializeField] protected GraphicsComponent graphics_;
    [SerializeField] protected AudioComponent audio_;

    [Header("Propiedades")] // no me gusta que sean public pero sino no se pueden acceder desde componentes que heredan!
    public float startingX = 0;
    public float startingY = 0;

    protected virtual void Start()
    {
        // componentes son asignados en el editor, por lo que no es necesario asignarlos aqui
        //Debug.Log("GameEntity: componentes: " + input_ + ", " + movement_ + ", " + physics_ + ", " + graphics_);
    }

    protected virtual void Update()
    {
        if (input_ != null) input_.ProcessInput();
        if (movement_ != null) movement_.UpdateState();
        if (graphics_ != null) graphics_.UpdateGraphics();
    }

    protected virtual void FixedUpdate()
    {
        if (physics_ != null) physics_.FixedUpdateState();
    }
}
