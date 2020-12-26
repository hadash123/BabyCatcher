using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AfterOpenDelegate(int rand);

public enum WindowsStatus
{
    OPEN,
    CLOSE,
}

public class Window : MonoBehaviour
{
    [Range( 0 , 10)]
    public int FireTimeAnim;
    [Range(0, 2)]
    public float FireSpeed;
    int ID;

    public GameObject fire;

    public Sprite Close;
    public Sprite Open;

    SpriteRenderer renderer;
    AfterOpenDelegate DropBaby;
    Vector3 finalPosition;

    WindowsStatus status = WindowsStatus.CLOSE;

    // Start is called before the first frame update
    void Start()
    {
        finalPosition = fire.transform.position;
        renderer = this.gameObject.GetComponent<SpriteRenderer>();

    }

    public void SetDelegate(AfterOpenDelegate drop, int id)
    {
        DropBaby = drop;
        this.ID = id;
    }

    public void ChangeWindowStatus()
    {
        status = (status == WindowsStatus.CLOSE) ? WindowsStatus.OPEN : WindowsStatus.CLOSE;
        renderer.sprite = (status == WindowsStatus.CLOSE) ? Close : Open;
        fire.SetActive(status == WindowsStatus.OPEN);
        fire.transform.position = finalPosition;
        MoveFireAnimation();
    }

    public WindowsStatus GetWindowsStatus()
    {
        return status;
    }

    void MoveFireAnimation()
    {
        if(status == WindowsStatus.OPEN)
        {
            StartCoroutine("MoveFireCorutine");
        }
        else
        {
            StopCoroutine("MoveFireCorutine");
        }
    }

    IEnumerator MoveFireCorutine()
    {
        finalPosition = fire.transform.position;
        fire.transform.position = fire.transform.position - new Vector3(0.5f, 0, 0);
        float time = 0;
        bool babyDropped = false;
        while(time<=FireTimeAnim)
        {
            time += Time.deltaTime;
            float step = FireSpeed * Time.deltaTime;
            fire.transform.position = Vector3.MoveTowards(fire.transform.position, finalPosition, step);
            yield return null;
            if (time >= FireTimeAnim * 0.8f && !babyDropped)
            {
                DropBaby(ID);
                babyDropped = true;
            }

        }
        
        ChangeWindowStatus();
        StopCoroutine("MoveFireCorutine");
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelInfoController.GetSingletone.IsPause && this.status == WindowsStatus.OPEN)
        {
            this.ChangeWindowStatus();
            
        }

    }
}
