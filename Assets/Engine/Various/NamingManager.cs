using Lexic;
using UnityEngine;
using UnityEditor;

public class NamingManager : MonoBehaviour
{
    #region MANAGER INITIALIZATION

    /// <summary>
    /// Can't access static variables and methods from inspector. So we use static instance to do that.
    /// </summary>
    public static NamingManager i;

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

    public NameGenerator maleNameGenerator, femaleNameGenerator;

    public string GetRandomHeroName(SexType sex) =>
        sex switch
        {
            SexType.Male => maleNameGenerator.GetNextRandomName(),
            SexType.Female => femaleNameGenerator.GetNextRandomName(),
            _ => string.Empty
        };
}