using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;

    public ParticleSystem[] myParticles;
    public AudioSource[] audioDrops;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(this.gameObject);
        }
    }
    //private void Start()
    //{
    //    CameraManager.Instance.SetSequencerPhotonView(GetComponent<PhotonView>());
    //}

    [PunRPC]
    public void EmitParticles(int index) 
    {
        myParticles[index].Play();
        audioDrops[index].Play();
    }
}
