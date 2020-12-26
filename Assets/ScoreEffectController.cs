using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEffectController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaytForDestroy());
    }
    IEnumerator WaytForDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
