using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoController : MonoBehaviour
{
    public Text scoreLable;
    public Transform scoreEffect;
    public GameObject PuseMenu;

    public int levelScore = 10;
    public int lifes = 5;
    bool isanimated = false;

    public bool gameOverBoolean = false;

    [Header("Baby respawn Time duration :")]
    [Range(0.25f, 25.0f)]
    public float Respowne;
    [Range(5, 80)]
    public int ChildrenDropPower = 25;

    [Header("Trampoline Controll Variables :")]
    [Range(2.0f, 18.0f)]
    public float TramplinePanchForce  = 2.5f;
    [Range(0.1f,1.0f)]
    public float ForceFriction = 0.25f;
    [Range(50, 200)]
    public int TramppolineElasticity = 50;

    [Header("BonusController :")]
    [Range(1, 10)]
    public float BonusTimeLifeDuration = 4;
    [Range(1, 100)]
    public float BonusChanse = 20;
    [Range(10f, 180)]
    public int SpeedPantchPower = 80;
    [Range(10f, 100f)]
    public int SlowPantchPower = 50;

    [Header("Player Controller :")]
    public ControllerType controllerType;
    public GameObject ArrowLeft;
    public GameObject ArrowRight;


    public bool IsPause = false;


    static LevelInfoController controller;

    // Start is called before the first frame update
    void Awake()
    {
        controllerType = GameModeController.CONTROLTYPE;
        ArrowLeft.SetActive(GameModeController.CONTROLTYPE == ControllerType.BUTTONS);
        ArrowRight.SetActive(GameModeController.CONTROLTYPE == ControllerType.BUTTONS);

        if (SoundController.IsInitialized)
            SoundController.GetSingleton.NormalizeSound();
        controller = this;
        controller.levelScore = 0;
        controller.lifes = 5;
        scoreLable.text = controller.levelScore.ToString();
    }
    public static LevelInfoController GetSingletone
    {
        get{
            return controller;
        }
    }

    public bool IsInitialized
    {
        get
        {
            return controller == null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isanimated)
        {
            StartCoroutine(ChangeScoreAnimate());
        }
        PuseMenu.SetActive(IsPause);
        if (Input.GetKeyDown(KeyCode.Escape)) IsPause = !IsPause;
    }


    IEnumerator ChangeScoreAnimate()
    {
        isanimated = true;
        int camtionScote = int.Parse(scoreLable.text);
        while (camtionScote < LevelInfoController.GetSingletone.levelScore)
        {
            camtionScote++;
            scoreLable.text = camtionScote.ToString();
            yield return new WaitForSeconds(0.05f);
        }
        isanimated = false;
    }

    /*
     * Show "+10" Score effect after Baby has done his way to MDA
     * */
    public void ShowScrollEffect()
    {
        Transform element = Instantiate(scoreEffect, scoreEffect.localPosition, Quaternion.identity);
        element.SetParent(this.transform);
        element.localScale = new Vector3(1,1,1);
        element.localPosition = new Vector3(350, -100, 0);
    }
}
