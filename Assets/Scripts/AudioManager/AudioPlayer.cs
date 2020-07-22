using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public int randomSound;
    public AudioSource currentAudio;

    // Start is called before the first frame update
    void Start()
    {
        AmbientSelection();
    }
    // Update is called once per frame
    void Update()
    {
        if (!currentAudio.isPlaying)
        {
            AmbientSelection();
        }
    }
    public void AmbientSelection()
    {
        randomSound = Random.Range(0, FindObjectOfType<AudioManager>().ambientSounds.Length);
        FindObjectOfType<AudioManager>().PlayLocalAmbient(FindObjectOfType<AudioManager>().ambientSounds[randomSound].name);
        FindObjectOfType<AudioManager>().PlayOnServerAmbient(FindObjectOfType<AudioManager>().ambientSounds[randomSound].name);
        currentAudio = FindObjectOfType<AudioManager>().ambientSounds[randomSound].source;
    }
}
