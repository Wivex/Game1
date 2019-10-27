public class Enemy : Unit
{
    internal EnemyData data;

    internal Enemy(EnemyData data)
    {
        this.data = data;
        InitData(data);
    }

    public override void Kill()
    {
        //if (!looting)
        //{
        //    // set up item transfer animation
        //    looting = true;
        //    SpawnLoot();
        //    // show "dead" status icon
        //    //expedition.expPreviewPanel.enemyStatusIcon.enabled = true;
        //    //// start cycles of loot transfer
        //    //expedition.expPreviewPanel.lootAnim.SetTrigger(AnimationTrigger.StartTransferLoot.ToString());
        //    //// make combat icon disappear
        //    //expedition.expPreviewPanel.interAnim.SetTrigger(AnimationTrigger.EndEncounter.ToString());
        //}

        //// loot transfer process (run before each cycle)
        //if (lootDrops.Count > 0)
        //{
        //    var item = lootDrops.FirstOrDefault();
        //    //expedition.expPreviewPanel.lootIcon.sprite = item.icon;
        //    // lock situation Updater until animation ends
        //    //state = SituationState.RunningAnimation;
        //    lootDrops.Remove(item);
        //}
        //else
        //{
        //    // stop animating item transfer
        //    //expedition.expPreviewPanel.lootAnim.SetTrigger(AnimationTrigger.StopTransferLoot.ToString());
        //    //// hero continue travelling
        //    //expedition.expPreviewPanel.heroAnim.SetTrigger(AnimationTrigger.EndEncounter.ToString());
        //    //// hide enemy icon
        //    //expedition.expPreviewPanel.eventAnim.SetTrigger(AnimationTrigger.EndEncounter.ToString());
        //    //Resolve();
        //}
    }
}