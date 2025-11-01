public class GazerController : GameEntity
{
    private bool isGazerMovingUp = false;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool GetIsGazerMovingUp()
    {
        return isGazerMovingUp;
    }

    public void SetIsGazerMovingUp(bool isGazerMovingUp)
    {
        this.isGazerMovingUp = isGazerMovingUp;
    }
}