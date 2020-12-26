using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Image[] images = new Image[5];
    public GameOver gameover;

    int lifeBarControllvariable = 5;
    // Start is called before the first frame update
    void Start()
    {
        lifeBarControllvariable = 5;
    }

    // Update is called once per frame
    //
    void Update()
    {
        if(lifeBarControllvariable> 0 && LevelInfoController.GetSingletone.lifes < lifeBarControllvariable)
        {
            images[lifeBarControllvariable-1].color = new Color(1, 1, 1, 0.35f);
            lifeBarControllvariable--;
            if(lifeBarControllvariable == 0)
            {
                gameover.Show();
            }
        }

        if(LevelInfoController.GetSingletone.lifes > lifeBarControllvariable)
        {
            images[lifeBarControllvariable ].color = new Color(1, 1, 1, 1);
            lifeBarControllvariable++;
        }
    }

}
