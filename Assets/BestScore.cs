using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BestScore : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = this.gameObject.GetComponent<Text>();
        StartCoroutine("GetScore");
    }

    IEnumerator GetScore()
    {
        while (!SharedVariables.IsInitialized)
            yield return null;

        text.text = SharedVariables.GetSingleton.maxScore.ToString();
    }
}

