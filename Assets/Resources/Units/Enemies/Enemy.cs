public class Enemy : Unit
{
    internal EnemyData data;

    internal override string Name => data.name;

    internal Enemy(EnemyData data) : base (data)
    {
        this.data = data;
        tactics = data.tactics;
    }

    public override void Kill()
    {
        //if (!looting)
        //{
        //    // set up item transfer animation
        //    looting = true;
        //    SpawnLoot();
        //    // show "dead" status icon
        //    //mission.expPreviewPanel.enemyStatusIcon.enabled = true;
        //    //// start cycles of loot transfer
        //    //mission.expPreviewPanel.lootAnim.SetTrigger(AnimationTrigger.StartTransferLoot.ToString());
        //    //// make combat icon disappear
        //    //mission.expPreviewPanel.interAnim.SetTrigger(AnimationTrigger.EndEncounter.ToString());
        //}

        //// loot transfer process (run before each cycle)
        //if (lootDrops.Count > 0)
        //{
        //    var item = lootDrops.FirstOrDefault();
        //    //mission.expPreviewPanel.movObjImage.sprite = item.icon;
        //    // lock situation Updater until animation ends
        //    //state = SituationState.RunningAnimation;
        //    lootDrops.Remove(item);
        //}
        //else
        //{
        //    // stop animating item transfer
        //    //mission.expPreviewPanel.lootAnim.SetTrigger(AnimationTrigger.StopTransferLoot.ToString());
        //    //// hero continue travelling
        //    //mission.expPreviewPanel.heroAnim.SetTrigger(AnimationTrigger.EndEncounter.ToString());
        //    //// hide enemy icon
        //    //mission.expPreviewPanel.eventAnim.SetTrigger(AnimationTrigger.EndEncounter.ToString());
        //    //Resolve();
        //}
    }
}