using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickController : MonoBehaviour
{
    public void PlayButtonPress()
    {
        SceneManager.LoadScene("Canvas");
    }
}
