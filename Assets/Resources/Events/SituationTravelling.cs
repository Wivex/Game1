public class SituationTravelling : Situation
{
    public SituationTravelling(Expedition expedition) : base(expedition)
    {
        expedition.expeditionPanel.UpdateLog($"Travelling trough {expedition.location.name}");
        type = SituationType.Travelling;
        readyForNewSituation = true;
    }

    public override void Update()
    {
    }
}