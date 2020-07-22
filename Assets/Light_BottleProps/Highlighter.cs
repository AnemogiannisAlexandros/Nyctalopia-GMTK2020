using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviourPun
{
    public Material mat;
    private MeshRenderer renderer;
    AudioSource source;
    
    [SerializeField] AudioClip clip;
    [SerializeField] Light thisLightControl;

    public float outlineValue;
    public float onExitValue;
    public float MaxTimeFlashing;
    float timeflashing = 0;
    float minWaitTime = 1;
    float maxWaitTime = 1.5f;

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        source = GetComponent<AudioSource>();
        renderer.materials[1].SetFloat("_Outline", onExitValue);
    }

    private void OnMouseEnter()
    {
        renderer.materials[1].SetFloat("_Outline", outlineValue);
    }

    private void OnMouseExit()
    {
        renderer.materials[1].SetFloat("_Outline", onExitValue);
    }

    private void OnMouseDown()
    {
        photonView.RPC("DoLights", RpcTarget.All);
    }

    [PunRPC]
    public void DoLights() 
    {
        StartCoroutine(playLight());
    }

    IEnumerator playLight()
    {
        source.clip = clip;

        while(timeflashing <= MaxTimeFlashing)
        {
            Debug.Log(timeflashing);
            var tmp = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(tmp);
            timeflashing += tmp;
            thisLightControl.enabled = !thisLightControl.enabled;
            if (thisLightControl.enabled) {
                source.pitch = Random.Range(0.93f, 1.0f);
                
                source.PlayOneShot(clip); };
        }

        timeflashing = 0;
        thisLightControl.enabled = false;
    }
}
