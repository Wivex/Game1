using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    public ExpeditionPanelDrawer expPanelDrawer;
    
    public FloatingText floatingTextPrefab;

    /// <summary>
    /// destroys all children objects (clean up prefab templates)
    /// </summary>
    /// <param name="obj">Parent object</param>
    public static void DestroyChildren(MonoBehaviour obj)
    {
        // clean up panel from debug leftovers
        while (obj.transform.childCount > 0)
        {
            Destroy(obj.transform.GetChild(0).gameObject);
        }
    }

    //default initialization of Singleton instance
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of it.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
}