using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Audio;
using System;
public class AudioManager : MonoBehaviourPun
{
    public AmbientSound[] ambientSounds;
    public AudioSource[] fxSounds;
    public static AudioManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(this.gameObject);
        }
        foreach(AmbientSound am in ambientSounds)
        {
            am.source = gameObject.AddComponent<AudioSource>();
            am.source.hideFlags = HideFlags.HideInInspector;
            am.source.clip = am.clip;
            am.source.volume = am.volume;
            am.source.pitch = am.pitch;
            am.source.loop = am.loop;
            am.source.playOnAwake = am.playOnAwake;       
        }
    }

    private void Start()
    {
        photonView.RPC("PlayOnServerFX",RpcTarget.All,0);
    }

    //---------Fx----------------
    public void PlayLocalFX(int fxNum)
    {
        fxSounds[fxNum].Play();
    }
    [PunRPC]
    public void PlayOnServerFX(int fxNum)
    {
        fxSounds[fxNum].Play();
    }
    //---------Ambient----------------
    public void PlayLocalAmbient(string name) 
    {
        AmbientSound a = Array.Find(ambientSounds, sound => sound.name == name);
        if (a.isLocal)
        {
            if (a == null)
            {
                return;
            }
        }
        a.source.Play();        
    }
    [PunRPC]
    public void PlayOnServerAmbient(string name) 
    {
        AmbientSound a = Array.Find(ambientSounds, sound => sound.name == name);
        
        if (a == null)
        {
            if (a.onServer)
            {
                return;
            }
        }
        a.source.Play();
    }
}
