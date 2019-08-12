using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region MANAGER INITIALIZATION

    /// <summary>
    /// Can't access static variables and methods from inspector. So we use static instance to do that.
    /// </summary>
    public static UIManager statics;

    //default initialization of Singleton instance
    void Awake()
    {
        //Check if instance already exists
        if (statics == null)
            //if not, set instance to this
            statics = this;
        //If instance already exists and it's not this:
        else if (statics != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of it.

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
    }

    #endregion

    public ExpeditionPanelDrawer expPanelDrawer;
    public MayorPanelDrawer mayorPanelDrawer;
    public TavernPanelDrawer tavernPanelDrawer;
    public FloatingText floatingTextPrefab;
}