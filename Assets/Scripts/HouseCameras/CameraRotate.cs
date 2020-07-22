using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    enum CameraState 
    {
        Rotating,
        Still
    }
    public Transform jumpscareSpawn;
    [SerializeField]
    float minMaxRotationAngle;
    [SerializeField]
    [Tooltip("Figher Values result in slower Rotation")]
    float rotateSpeedDivider;
    CameraState currentCameraState;
    [SerializeField]
    float cameraStillTimer;
    private float yRotate;
    private float timer;
    private float resetTimer;
    // Start is called before the first frame update
    void Start()
    {
        yRotate = 0;
        timer = 0;
        resetTimer = 0.5f;
        currentCameraState = CameraState.Still;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentCameraState);
        if (currentCameraState == CameraState.Still)
        {
            timer += Time.deltaTime;
            if (timer >= cameraStillTimer) 
            {
                currentCameraState = CameraState.Rotating;
                timer = 0;
            }
        }
        else if (currentCameraState == CameraState.Rotating) 
        {
            resetTimer += Time.deltaTime / rotateSpeedDivider;
            yRotate = Mathf.PingPong(resetTimer, 1);
            transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, -minMaxRotationAngle, 0), Quaternion.Euler(0, minMaxRotationAngle, 0), yRotate);
            if (
                (Mathf.Round(transform.localRotation.eulerAngles.y) <= 360 - minMaxRotationAngle 
                && Mathf.Round(transform.localRotation.eulerAngles.y) > minMaxRotationAngle) ||
                (Mathf.Round(transform.localRotation.eulerAngles.y) >= minMaxRotationAngle 
                && Mathf.Round(transform.localRotation.eulerAngles.y)< 360 - minMaxRotationAngle)
                )
            {
                resetTimer += 0.01f;
                currentCameraState = CameraState.Still;
            }
        }
    }
}
