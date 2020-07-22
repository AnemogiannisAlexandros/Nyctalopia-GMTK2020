using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

public class CameraManager : MonoBehaviourPun
{
    public static CameraManager Instance;

    [SerializeField]
    private List<Camera> myAvailableCameras;
    [SerializeField]
    int currentCameraIndex = 0;
    [SerializeField]
    float queueTimerInterval;
    [SerializeField]
    PhotonView rpcPhotonview;
    private bool allowMessageThrough;
    private float startingTime;
    public UnityEvent OnCameraChanged;

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
    }
    public void SetSequencerPhotonView(PhotonView photonView) 
    {
        rpcPhotonview = photonView;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient || !PhotonNetwork.IsConnected) 
        {
            for(int i =0;i<transform.childCount;i++) 
            {
                if (transform.GetChild(i).GetChild(0).TryGetComponent<Camera>(out Camera cam)) 
                {
                    myAvailableCameras.Add(cam);
                    cam.gameObject.SetActive(false);
                }
            }
            myAvailableCameras[currentCameraIndex].gameObject.SetActive(true);
            allowMessageThrough = false;
            startingTime = queueTimerInterval;
            OnCameraChanged.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient || !PhotonNetwork.IsConnected)
        {
            InputChecks();
        }
    }

    public Camera GetCurrentlyActiveCamera() 
    {
        return myAvailableCameras[currentCameraIndex];
    }
    public int GetCurrentCameraIndex() 
    {
        return currentCameraIndex;
    }

    public void InputChecks() 
    {
        queueTimerInterval -= Time.deltaTime;
        if (queueTimerInterval <= 0) 
        {
            allowMessageThrough = true;
            queueTimerInterval = -0.01f;
        }
        if (allowMessageThrough) 
        {
            if (InputManager.Instance.GetInputMethod().SendQueueStraight()) 
            {
                Debug.Log("Going Straight");
                allowMessageThrough = false;
                queueTimerInterval = startingTime;
                rpcPhotonview.RPC("EmitParticles", RpcTarget.All, 0);
            }
            else if (InputManager.Instance.GetInputMethod().SendQueueRight())
            {
                Debug.Log("Going Right");
                allowMessageThrough = false;
                queueTimerInterval = startingTime;
                rpcPhotonview.RPC("EmitParticles", RpcTarget.All, 1);

            }
            else if (InputManager.Instance.GetInputMethod().SendQueueLeft())
            {
                Debug.Log("Going Left");
                allowMessageThrough = false;
                queueTimerInterval = startingTime;
                rpcPhotonview.RPC("EmitParticles", RpcTarget.All, 2);

            }
            else if (InputManager.Instance.GetInputMethod().SendQueueBackward())
            {
                Debug.Log("Going Back");
                allowMessageThrough = false;
                queueTimerInterval = startingTime;
                rpcPhotonview.RPC("EmitParticles", RpcTarget.All, 3);
            }
        }
        if (InputManager.Instance.AllowCameraInput) 
        {
            if (InputManager.Instance.GetInputMethod().CameraSwitchLeft())
            {
                myAvailableCameras[currentCameraIndex].gameObject.SetActive(false);
                if (currentCameraIndex == myAvailableCameras.Count - 1)
                {
                    currentCameraIndex = 0;
                }
                else
                {
                    currentCameraIndex++;
                }
                OnCameraChanged.Invoke();
                myAvailableCameras[currentCameraIndex].gameObject.SetActive(true);
            }
            else if (InputManager.Instance.GetInputMethod().CameraSwitchRight())
            {
                myAvailableCameras[currentCameraIndex].gameObject.SetActive(false);

                if (currentCameraIndex == 0)
                {
                    currentCameraIndex = myAvailableCameras.Count - 1;
                }
                else
                {
                    currentCameraIndex--;
                }
                OnCameraChanged.Invoke();
                myAvailableCameras[currentCameraIndex].gameObject.SetActive(true);
            }
        }
    }
}
