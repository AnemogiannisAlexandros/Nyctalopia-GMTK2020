using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class telephoneHighlighter : MonoBehaviourPun
{
    public Material mat;
    private MeshRenderer renderer;
    AudioSource source;
    ParticleSystem system;

    [SerializeField] AudioClip clip;

    bool isRadioEnabled = false;

    public float outlineValue;
    public float onExitValue;
    public float minTimeEmission = 1;
    public float maxTimeEmission = 3;
    float timeflashing = 0;
    float minWaitTime = 1;
    float maxWaitTime = 1.5f;

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        source = GetComponent<AudioSource>();
        system = GetComponentInChildren<ParticleSystem>();
        mat = renderer.materials[0];
        source.clip = clip;
        system.Stop();
        renderer.materials[1].SetFloat("_Outline", onExitValue);

    }

    private void OnMouseEnter()
    {
        Debug.Log("TV");
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
        isRadioEnabled = !isRadioEnabled;

        if (isRadioEnabled)
        {
            renderer.materials[1].SetFloat("_Outline", 0.03f);
            StartCoroutine(playTV());
            system.Play();
        }
    }

    IEnumerator playTV()
    {
        source.Play();
        while (isRadioEnabled)
        {
            yield return null;
        }

        source.Stop();
        system.Stop();

        yield return null;
    }
}
