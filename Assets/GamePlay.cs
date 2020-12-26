using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public Transform babyPrefab;
    public Window[] windows = new Window[4];

    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > LevelInfoController.GetSingletone.Respowne && !LevelInfoController.GetSingletone.gameOverBoolean)
        {
            int rand = ChouseRandomWindow();
            windows[rand].SetDelegate(RandomDropBaby, rand);
            windows[rand].ChangeWindowStatus();
            time = 0;
        }
        // When GameOver Stop Respown Babies
        if(LevelInfoController.GetSingletone.gameOverBoolean)
        {
            for(int i = 0; i < 4;i++)
            {
                if(windows[i].GetWindowsStatus() == WindowsStatus.OPEN)
                {
                    windows[i].ChangeWindowStatus();
                }
            }
            this.StopAllCoroutines();
        }
        
    }

    int ChouseRandomWindow()
    {
        int rand = Random.Range(0, 4);
        if(windows[rand].GetWindowsStatus() == WindowsStatus.OPEN)
        {
            rand = ChouseRandomWindow();
        }
        return rand;
    }

    void RandomDropBaby(int rand)
    {
        Transform element = Instantiate(babyPrefab, windows[rand].transform.position, Quaternion.identity);
        element.gameObject.SetActive(true);
        element.GetComponent<Baby>().SetForce(Random.Range(25, LevelInfoController.GetSingletone.ChildrenDropPower), 
            Random.Range(LevelInfoController.GetSingletone.ChildrenDropPower/2, LevelInfoController.GetSingletone.ChildrenDropPower));
        element.GetComponent<Baby>().Drop();
        
    }
    
}
