using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{

    public GameObject text;
    public Text label;
    public Animator animator;
    public Text Level;

    float respawnChangeTimeStep = 0.2f;
    int currentLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        label.text = "LEVEL " + currentLevel.ToString();
        StartCoroutine(LevelUpCorutine());
    }

    IEnumerator LevelUpCorutine()
    {
        yield return new WaitForSeconds(1.5f);
        text.SetActive(false);
        Level.text = "LEVEL : " + currentLevel.ToString();
        yield return new WaitForSeconds(60);
        currentLevel++;
        label.text = "LEVEL " + currentLevel.ToString();
        text.SetActive(true);
        if(LevelInfoController.GetSingletone.Respowne > 1)
            LevelInfoController.GetSingletone.Respowne -= this.respawnChangeTimeStep;
        if(SoundController.IsInitialized)
        {
            SoundController.GetSingleton.PlayFaster();
        }
        StartCoroutine(LevelUpCorutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
