using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropItemType
{
    Baby = 0,
    Speed = 1,
    Slow = 2,
    LifeBonus = 3,
}

public class Baby : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[4];

    DropItemType type = DropItemType.Baby;

    [Range(25, 100)]
    public int xForce;
    [Range(25, 100)]
    public int yForce;

    CircleCollider2D collider;
    SpriteRenderer renderer;
    public Rigidbody2D body;

    Vector3 forvard;

    public DropItemType GetBonusType
    {
        get
        {
            return type;
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        //forvard = new Vector3(xForce, yForce, 0);
        collider = this.gameObject.GetComponent<CircleCollider2D>();
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
        this.gameObject.SetActive(false);
        renderer.sprite = sprites[(int)type];
        if (Random.Range(0, 100) <= LevelInfoController.GetSingletone.BonusChanse)
        {
            SetAsABonus();
        }
    }
    /*Set Element as Bonus
     * */

    private void Update()
    {
        if(LevelInfoController.GetSingletone.IsPause)
        {
            Destroy(this.gameObject);
        }
    }
    public void SetAsABonus()
    {
        int rand = Random.Range(1, 4);
        type = (DropItemType)rand;
        renderer.sprite = sprites[rand];
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    /*Set Force of Dropping
     * */
    public void SetForce(int x_force, int y_force)
    {
        xForce = x_force;
        yForce = y_force;
        forvard = new Vector3(x_force, y_force, 0);

    }


    /* Drop Spawned element
     * playsound - Set This False if we dont need to play drop sound
     * */
    public void Drop(bool playsound = true)
    {
        this.gameObject.SetActive(true);
        body = this.gameObject.GetComponent<Rigidbody2D>();
        body.AddForce(forvard);

        if (playsound && SoundController.IsInitialized) { 
            if (this.type == DropItemType.Baby)
                SoundController.GetSingleton.PlaySound(SoundType.Baby);
            else
                SoundController.GetSingleton.PlaySound(SoundType.Drop);
        }

    }

    /*Check Respawn Element For Collision
     * */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.type == DropItemType.Baby)
        {
            

            
            if (collision.gameObject.name.Equals("trampoline"))
            {
                forvard = new Vector3(LevelInfoController.GetSingletone.ChildrenDropPower, LevelInfoController.GetSingletone.TramppolineElasticity, 0);
                if (SoundController.IsInitialized)
                    SoundController.GetSingleton.PlaySound(SoundType.Trampoline);
                Drop(false);
            }
            if (collision.gameObject.name.Equals("MDA"))
            {
                LevelInfoController.GetSingletone.levelScore += 10;
                LevelInfoController.GetSingletone.ShowScrollEffect();
                Destroy(this.gameObject);
                if (SoundController.IsInitialized)
                    SoundController.GetSingleton.PlaySound(SoundType.Success);
            }
        }
        if (collision.gameObject.name.Equals("DownLine"))
        {
            if (this.type == DropItemType.Baby)
                LevelInfoController.GetSingletone.lifes--;
            Destroy(this.gameObject);
            if(SoundController.IsInitialized)
                SoundController.GetSingleton.PlaySound(SoundType.Crash);
        }
        if ((collision.gameObject.tag == "Baby"))
        {
            Physics2D.IgnoreCollision(collider, collision.transform.GetComponent<Collider2D>());
        }
    }
}
