using UnityEngine;


public class GazerInputComponent : InputComponent
{
    [SerializeField] private GazerController gazer;
    [SerializeField] private float bottomY = -3f;

    public override void ProcessInput()
    {
        bool isGazerMovingUp = gazer.GetIsGazerMovingUp();

        // si el gazer se esta moviendo hacia arriba y llego al tope superior
        if (isGazerMovingUp && transform.position.y >= gazer.startingY) gazer.SetIsGazerMovingUp(false);

        // si el gazer se esta moviendo hacia abajo y llego al tope inferior
        else if (!isGazerMovingUp && transform.position.y <= bottomY) gazer.SetIsGazerMovingUp(true);
    }
}