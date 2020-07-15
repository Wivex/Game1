using System.Collections.Generic;
using System.Linq;
using Lexic;
using SubjectNerd.Utilities;
using UnityEngine;

internal class MissionSetUp
{
    internal Hero hero;
    internal Dictionary<ZoneData, int> route = new Dictionary<ZoneData, int>();

    internal void Reset()
    {
        hero = null;
        route = new Dictionary<ZoneData, int>();
    }
}

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

    [Reorderable(ReorderableNamingType.ReferencedObjectName)]
    public List<ZoneData> debugMissionRouteZones;

    public Transform MissionsOverviewContentPanel;

    #endregion

    internal static List<Mission> missions = new List<Mission>();
    internal static MissionSetUp missionSetUp = new MissionSetUp();

    public void StartNewMissionDebug()
    {
        missionSetUp.Reset();
        missionSetUp.hero = TownManager.GenerateRandomHero();
        debugMissionRouteZones.ForEach(zone => missionSetUp.route.Add(zone, 10));
        StartSetUpMission();
    }

    public void StartSetUpMission()
    {
        missions.Add(new Mission(missionSetUp));
        missionSetUp.Reset();
        //UIManager.misPanelManager.NewPreviewPanel(missions.Last());
    }

    public void ResetSetUpMission()
    {
        missionSetUp.Reset();
    }
}