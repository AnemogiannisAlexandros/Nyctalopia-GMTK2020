using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using Photon.Pun;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text cameraNumber;
    [SerializeField]
    private Canvas mainCanvas;
    [SerializeField]
    private VideoPlayer staticCameraClip;

    public static UIManager Instance;

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

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient && PhotonNetwork.IsConnected)
        {
            gameObject.SetActive(false);
            return;
        }
        mainCanvas = GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCanvasInfo()
    {
        Camera myCurrentCamera = CameraManager.Instance.GetCurrentlyActiveCamera();
        mainCanvas.worldCamera = myCurrentCamera;
        mainCanvas.planeDistance = myCurrentCamera.nearClipPlane + 0.001f;
        StopCoroutine("UpdateCanvas");
        StartCoroutine("UpdateCanvas",myCurrentCamera);
    }
    private IEnumerator UpdateCanvas(Camera myCurrentCamera)
    {
        InputManager.Instance.AllowCameraInput = false;
        staticCameraClip.gameObject.SetActive(true);
        staticCameraClip.targetCamera = myCurrentCamera;
        staticCameraClip.Play();
        yield return new WaitForSeconds((float)(staticCameraClip.clip.length/staticCameraClip.playbackSpeed));
        staticCameraClip.gameObject.SetActive(false);
        cameraNumber.text = "" + (CameraManager.Instance.GetCurrentCameraIndex() + 1);
        InputManager.Instance.AllowCameraInput = true;
    }
}
