using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    public void SetPause()
    {
        LevelInfoController.GetSingletone.IsPause = true;
    }

    public void UnsetPause()
    {
        LevelInfoController.GetSingletone.IsPause = false;
    }

}
