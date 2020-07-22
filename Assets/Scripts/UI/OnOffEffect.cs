using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnOffEffect : MonoBehaviour
{
    [SerializeField]
    private float frequency = 1;
    [SerializeField]
    Transform[] targets;

    private float timer;

    private void Update()
    {
        if (InputManager.Instance.AllowCameraInput)
        {
            timer += Time.deltaTime;
            if (timer > frequency)
            {
                foreach (Transform t in targets)
                {
                    t.gameObject.SetActive(!t.gameObject.activeInHierarchy);
                }
                timer = 0;
            }
        }
        else 
        {
            foreach (Transform t in targets)
            {
                t.gameObject.SetActive(false);
            }
        }
    }
}
