using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ControllerType
{
    SLICE,
    BUTTONS,
}

public class Trampoline : MonoBehaviour
{
    public Animator Fireman1;
    public Animator Fireman2;

    public Color RedColor; // Sloww bonus trampoline Color
    public Color GreenColor;// Speed bonus trampoline Color

    float RightBorder = 4.64f; // right Border trampoline out of move  
    float LeftBorder = -4.81f; // left Border trampoline out of move 

    public SpriteRenderer renderer;
    public Rigidbody2D body;

    float sideKof = 0;
    
    public float RealSpeed = 0;
    const float SPEED = 160;

    bool isMoved = false;
    int vectorOfPunchForce = 0;
    int vectorBackForce = 0;


    float widthScreen;
    public int  step;


    // Start is called before the first frame update
    //Initialize
    void Start()
    {
        //body = this.gameObject.GetComponent<Rigidbody2D>();
        step = LevelInfoController.GetSingletone.TramppolineElasticity;
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
        AnimatorSwither(Fireman1, 0);
        AnimatorSwither(Fireman2, 0);
    }


    /*
     * Stop Start Animaton Of Fireman. When our trampoline start move or stop
     * */

    public void AnimatorSwither(Animator animator, float speed)
    {
        AnimatorStateInfo animInfo = animator.GetCurrentAnimatorStateInfo(0);
        if(animInfo.IsTag("Fireman"))
        {
            animator.speed = speed;
        }

    }

    // Update is called once per frame
    /*
     * Trompline Conroll With Mouse Touch
     * */

    private void FixedUpdate()
    {
        if (Input.mousePosition.y > Screen.height * 0.7f) return;
        if (LevelInfoController.GetSingletone.IsPause) return;
        if (LevelInfoController.GetSingletone.gameOverBoolean) return;

        switch(GameModeController.CONTROLTYPE)
        {
            case ControllerType.SLICE:
                this.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, this.transform.position.y);
                break;
            case ControllerType.BUTTONS:
                
                Vector2 wordLength = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
                this.transform.position = new Vector2(this.transform.position.x + sideKof * wordLength.x*0.03f, this.transform.position.y);

                break;
        }
        
    }
    private void Update()
    {
        
        //Debug.Log(sideKof);
        
    }

    public void Move(int l)
    {
        sideKof = l;
        AnimatorSwither(Fireman1, 1);
        AnimatorSwither(Fireman2, 1);
    }

    public void Stop()
    {
        sideKof = 0;
        AnimatorSwither(Fireman1, 0);
        AnimatorSwither(Fireman2, 0);
    }


    /*
     * From Her wi look if our trampoline colised with bomus, and start bonuses
     * */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Baby"))
        {
            
            DropItemType type = collision.gameObject.GetComponent<Baby>().GetBonusType;
            
            // Bonus With Speed

            if (type == DropItemType.Speed || type == DropItemType.Slow) { 
                StopCoroutine("StartBonus");
                step = LevelInfoController.GetSingletone.TramppolineElasticity;
                LevelInfoController.GetSingletone.TramppolineElasticity = step;
                StartCoroutine("StartBonus",type);
            }

            // Life Bonus
            else if (type == DropItemType.LifeBonus && LevelInfoController.GetSingletone.lifes < 5)
            {
                LevelInfoController.GetSingletone.lifes++;
            }
            if(type != DropItemType.Baby)
            {
                Destroy(collision.gameObject);
                if(SoundController.IsInitialized)
                    SoundController.GetSingleton.PlaySound(SoundType.Success);
            }
        }
    }

    /*
     * Start bonus with Trampoline Movement speed
     * */
    IEnumerator StartBonus(DropItemType type)
    {
        
        switch (type)
        {
            case DropItemType.Speed:
                LevelInfoController.GetSingletone.TramppolineElasticity = LevelInfoController.GetSingletone.SpeedPantchPower;
                renderer.color = GreenColor;
                break;
            case DropItemType.Slow:
                LevelInfoController.GetSingletone.TramppolineElasticity = LevelInfoController.GetSingletone.SlowPantchPower ;
                renderer.color = RedColor;
                break;
        }
        yield return new WaitForSeconds(LevelInfoController.GetSingletone.BonusTimeLifeDuration);

        renderer.color = new Color(1, 1, 1, 1);
        LevelInfoController.GetSingletone.TramppolineElasticity = step;
    }
}
