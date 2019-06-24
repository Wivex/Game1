public class SituationTravelling : Situation
{
    public SituationTravelling(Expedition expedition) : base(expedition)
    {
        type = SituationType.Travelling;
        state = SituationState.Resolved;
    }

    public override void Update()
    {
    }
}