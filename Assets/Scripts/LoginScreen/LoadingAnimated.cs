using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LoadingAnimated : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    public string textBeforeDots;
    public void Start() 
    {
        StartCoroutine(TextAnimation());
    }
    private IEnumerator TextAnimation() 
    {
        while (true)
        {
            text.text = textBeforeDots + ".  ";
            yield return new WaitForSeconds(0.35f);
            text.text = textBeforeDots + ".. ";
            yield return new WaitForSeconds(0.35f);
            text.text = textBeforeDots + "...";
            yield return new WaitForSeconds(0.35f);
        }
    }
}
