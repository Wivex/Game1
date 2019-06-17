public class SituationTravelling : Situation
{
    public SituationTravelling(Expedition expedition) : base(expedition)
    {
        type = SituationType.Travelling;
    }

    public override void Update()
    {
    }
}