using Lexic;
using UnityEngine;
using UnityEditor;

public class NamingManager : MonoBehaviour
{
    #region MANAGER INITIALIZATION

    /// <summary>
    /// Can't access static variables and methods from inspector. So we use static instance to do that.
    /// </summary>
    public static NamingManager statics;

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

    public NameGenerator maleNameGenerator, femaleNameGenerator;

    public string GetRandomHeroName(Hero hero)
    {
        switch (hero.sexType)
        {
            case SexType.Male:
                return maleNameGenerator.GetNextRandomName();
            case SexType.Female:
                return femaleNameGenerator.GetNextRandomName();
            default:
                return string.Empty;
        }
    }
}