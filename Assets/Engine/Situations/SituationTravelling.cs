public class SituationTravelling : Situation
{
    public SituationTravelling(Expedition expedition) : base(expedition)
    {
        type = SituationType.Travelling;
        readyForNewSituation = true;
    }

    public override void Update()
    {
    }
}