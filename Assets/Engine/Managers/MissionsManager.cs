using System.Collections.Generic;
using System.Linq;
using Lexic;
using UnityEngine;

public class MissionsManager : MonoBehaviour
{
    #region STATIC REFERENCE INITIALIZATION

    /// <summary>
    /// Can't access static variables and methods from inspector. So we use static instance to do that.
    /// </summary>
    public static MissionsManager i;

    //default initialization of Singleton instance
    void Awake()
    {
        //Check if instance already exists
        if (i == null)
            //if not, set instance to this
            i = this;
        //If instance already exists and it's not this:
        else if (i != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of it.

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
    }

    #endregion

    #region SET IN INSPECTOR

    [Tooltip("Minimum time in seconds between events")]
    public int minGracePeriod;

    #endregion

    internal static List<Mission> missions = new List<Mission>();

    // public void StartNewMissionDebug()
    // {
    //     StartNewMission(TownManager.NewHeroDebug(),
    //         Resources.Load<ZoneData>("Locations/Outskirts/Outskirts"));
    // }

    public void StartNewMission(Hero hero, List<ZonePath> route)
    {
        // var mission = new Mission(hero, zone);
        // missions.Add(mission);
        // UIManager.expPanelManager.NewPreviewPanel(mission);
    }

    void Update()
    {
        missions.ForEach(mission => mission.Update());
    }
}