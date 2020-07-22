using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVBehaviour : MonoBehaviourPun
{
    public Material mat;
    private MeshRenderer renderer;
    AudioSource source;
    ParticleSystem system;

    [SerializeField] AudioClip clip;

    bool isTvEnabled = false;

    public float outlineValue;
    public float onExitValue;
    public float minTimeEmission=1;
    public float maxTimeEmission=3;
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
        isTvEnabled = !isTvEnabled;

        if (isTvEnabled)
        {
            StartCoroutine(playTV());
            system.Play();
        }
    }
    IEnumerator playTV()
    {
        source.Play();
        while (isTvEnabled)
        {
            float rand = Random.Range(minTimeEmission, maxTimeEmission);
            Debug.Log(rand);
            mat.SetVector("_EmissionColor", Color.white * 5f);
            yield return new WaitForSeconds(rand);
            float rand1 = Random.Range(minTimeEmission, maxTimeEmission);
            Debug.Log(rand1);
            mat.SetVector("_EmissionColor", Color.white * 1f);
            yield return new WaitForSeconds(rand1);
        }

        source.Stop();
        system.Stop();

        yield return null;
    }
}
