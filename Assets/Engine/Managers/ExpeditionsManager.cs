using System.Collections.Generic;
using System.Linq;
using Lexic;
using UnityEngine;

public class ExpeditionsManager : MonoBehaviour
{
    #region STATIC REFERENCE INITIALIZATION

    /// <summary>
    /// Can't access static variables and methods from inspector. So we use static instance to do that.
    /// </summary>
    public static ExpeditionsManager i;

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

    internal static List<Expedition> expeditions = new List<Expedition>();

    float oldCombatSpeed;

    public void StartNewExpeditionDebug()
    {
        StartNewExpedition(TownManager.NewHeroDebug(),
            Resources.Load<LocationData>("Locations/Outskirts/Outskirts"));
    }

    public void StartNewExpedition(Hero hero, LocationData location)
    {
        var exp = new Expedition(hero, location);
        expeditions.Add(exp);
        UIManager.expPanelManager.NewPreviewPanel(exp);
    }

    void Update()
    {
        expeditions.ForEach(exp => exp.Update());
    }
}