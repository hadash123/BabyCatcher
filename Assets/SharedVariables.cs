using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharedVariables : MonoBehaviour
{
    

    PlayerPrefs pref = new PlayerPrefs();
    public int maxScore;

    private static SharedVariables instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        if (PlayerPrefs.HasKey("Score"))
            maxScore = PlayerPrefs.GetInt("Score");
    }


    public static SharedVariables GetSingleton
    {
        get
        {
            return instance;
        }
    }

    public static bool IsInitialized
    {
        get
        {
            return instance != null;
        }
    }

    public void Sawe(int Score)
    {
        if(maxScore <Score)
        {
            PlayerPrefs.SetInt("Score", Score);
            maxScore = Score;
        }
    }

}
