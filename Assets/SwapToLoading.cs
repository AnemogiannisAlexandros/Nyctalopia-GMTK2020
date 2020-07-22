using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapToLoading : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitToLoad());
    }
    IEnumerator WaitToLoad() 
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
