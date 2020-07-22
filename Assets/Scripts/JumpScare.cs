using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public AudioSource NPCSoundSource;
    public Animator NPCAnim;
    public AudioSource JSSound1;
    public AudioSource JSSound2;
    // Start is called before the first frame update
    void Start()
    {
        NPCAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (NPCAnim.GetBool("Scaring"))
        {
            StartCoroutine(JumpScareCo());
        }
    }
    IEnumerator JumpScareCo()
    {
        NPCAnim.SetBool("Scaring", true);

        JSSound1.Play();
        JSSound2.Play();
        yield return new WaitForEndOfFrame();
        NPCAnim.SetBool("Scaring", false);
    }
}
