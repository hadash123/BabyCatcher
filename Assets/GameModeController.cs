using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeController : MonoBehaviour
{
    public static ControllerType CONTROLTYPE = ControllerType.SLICE;

    public Image Slice;
    public Image Buttons;

    public void SliceModeOn()
    {
        SetType(0);
    }

    public void ButtonsModeOn()
    {
        SetType(1);
    }

    public void SetType(int t)
    {

        Slice.color = new Color(1, 1, 1, 1);
        Buttons.color = new Color(1, 1, 1, 1);

        switch(t)
        {
            case 0:
                GameModeController.CONTROLTYPE = ControllerType.SLICE;
                Slice.color = new Color(0.5f, 0.5f, 0.5f, 1);
                break;
            case 1:
                GameModeController.CONTROLTYPE = ControllerType.BUTTONS;
                Buttons.color = new Color(0.5f, 0.5f, 0.5f, 1);
                break;
        }

    }
    private void Awake()
    {
        SetType(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
