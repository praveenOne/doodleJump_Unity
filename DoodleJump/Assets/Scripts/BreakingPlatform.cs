public class BreakingPlatform : Platform
{
    public override int OnStep()
    {
        Destroy(gameObject);
        return base.OnStep();
    }
}
