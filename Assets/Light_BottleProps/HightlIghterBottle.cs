using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HightlIghterBottle : MonoBehaviourPun
{


    public Material mat;
    private MeshRenderer renderer;
    AudioSource source;
    public GameObject Bottle;
    public GameObject particleSound;
    ParticleSystem particles;

    [SerializeField] AudioClip[] clip;


    private void Awake()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
        source = GetComponent<AudioSource>();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    private void OnMouseEnter()
    {
        renderer.materials[1].SetFloat("_Outline", 0.05f);
    }

    private void OnMouseExit()
    {
        renderer.materials[1].SetFloat("_Outline", 0.03f);
    }

    private void OnMouseDown()
    {
        photonView.RPC("PlayMouse",RpcTarget.All);
    }

    [PunRPC]
    public void PlayMouse() 
    {
        source.PlayOneShot(clip[Random.Range(0, clip.Length)]);
        Bottle.SetActive(false);
        particles.Play();
    }

}


