public class SituationTravelling : Situation
{
    public SituationTravelling(Expedition expedition) : base(expedition)
    {
        type = SituationType.Travelling;
        resolved = true;
    }

    public override void Update()
    {
    }
}