using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public Text ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        LevelInfoController.GetSingletone.gameOverBoolean = true;
        ScoreText.text = LevelInfoController.GetSingletone.levelScore.ToString();
        if(SharedVariables.IsInitialized)
        {
            SharedVariables.GetSingleton.Sawe(LevelInfoController.GetSingletone.levelScore);
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("Canvas");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainPage");
    }
}
