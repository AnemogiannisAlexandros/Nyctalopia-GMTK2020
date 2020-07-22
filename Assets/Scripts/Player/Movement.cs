using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Movement : MonoBehaviourPun
{

    [SerializeField]
    private float rotationTime,stepTime;
    [SerializeField]
    Camera playerCamera;
    [SerializeField]
    LayerMask obstacles;
    [SerializeField]
    Transform[] raycastOrigins;
    private Animator anim;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        CameraManager.Instance.SetSequencerPhotonView(FindObjectOfType<ParticleManager>().GetComponent<PhotonView>());
        if (PhotonNetwork.IsMasterClient || !PhotonNetwork.IsConnected)
        {
            playerCamera.gameObject.SetActive(false);
        }
        else 
        {
            playerCamera.gameObject.SetActive(true);
        }
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient || !PhotonNetwork.IsConnected) 
        {
            if (InputManager.Instance.AllowPlayerInput) 
            {
                if (InputManager.Instance.GetInputMethod().PlayerSwitchLeft())
                {
                    PlayerRotate(false);
                    //photonView.RPC("PlayerRotate", RpcTarget.All, false);
                }
                else if (InputManager.Instance.GetInputMethod().PlayerSwitchRight())
                {
                    PlayerRotate(true);

                    //photonView.RPC("PlayerRotate", RpcTarget.All, true);
                }
                else if (InputManager.Instance.GetInputMethod().PlayerMoveForward())
                {
                    MovePlayer(true);
                    //photonView.RPC("MovePlayer", RpcTarget.All, true);
                }
                else if (InputManager.Instance.GetInputMethod().PlayerMoveBackwards())
                {
                    MovePlayer(false);

                    //photonView.RPC("MovePlayer", RpcTarget.All, false);
                }
                else 
                {
                    anim.SetBool("IsWalking", false);
                }
            }
        }
    }
    [PunRPC]
    public void MovePlayer(bool forward) 
    {
        PlayerMove(forward);
        //StartCoroutine(PlayerMove(forward));
    }
    [PunRPC]
    public void PlayerRotate(bool right) 
    {
        RotatePlayer(right);
        //StartCoroutine(RotatePlayer(right));
    }

    void RotatePlayer(bool right)
    {
        if (InputManager.Instance.AllowPlayerInput)
        {
            if (right)
            {
                transform.localRotation *= Quaternion.Euler(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
            }
            else
            {
                transform.localRotation *= Quaternion.Euler(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
            }
        }
    }
    void PlayerMove(bool forward)
    {
        if (InputManager.Instance.AllowPlayerInput)
        {
            Vector3 direction;
            if (forward)
            {
                direction = transform.forward;

            }
            else
            {
                direction = -transform.forward;
            }
            Debug.Log(direction);
            controller.Move(direction * Time.deltaTime * movementSpeed);
            anim.SetBool("IsWalking", true);
        }
    }
    //IEnumerator PlayerMove(bool forward)
    //{

    //    InputManager.Instance.AllowPlayerInput = false;
    //    Vector3 startPosition = transform.position;
    //    Vector3 endPosition = transform.position;
    //    if (forward)
    //    {
    //        RaycastHit objectHit;
    //        Physics.Simulate(Time.fixedDeltaTime);
    //        for (int i = 0; i < raycastOrigins.Length; i++)
    //        {
    //            if (Physics.Raycast(raycastOrigins[i].position, transform.forward, out objectHit, 2, obstacles))
    //            {
    //                // Debug.Log(Vector3.Distance(raycastOrigins[i].position, objectHit.point));
    //                if (Vector3.Distance(raycastOrigins[i].position, objectHit.point) <= 1.5f)
    //                {
    //                    InputManager.Instance.AllowPlayerInput = true;
    //                    yield break;
    //                }
    //            }
    //            endPosition = transform.position + transform.forward;
    //        }
    //    }
    //    else
    //    {
    //        RaycastHit objectHit;
    //        Physics.Simulate(Time.fixedDeltaTime);
    //        for (int i = 0; i < raycastOrigins.Length; i++)
    //        {
    //            if (Physics.Raycast(raycastOrigins[i].position, -transform.forward, out objectHit, 2, obstacles))
    //            {
    //                //Debug.Log(Vector3.Distance(raycastOrigins[i].position, objectHit.point));
    //                if (Vector3.Distance(raycastOrigins[i].position, objectHit.point) <= 1.5f)
    //                {
    //                    InputManager.Instance.AllowPlayerInput = true;
    //                    yield break;
    //                }
    //            }
    //            endPosition = transform.position - transform.forward;
    //        }
    //    }
    //    anim.SetTrigger("Walk");
    //    float interpolationTimer = 0;
    //    while (interpolationTimer <= stepTime)
    //    {
    //        interpolationTimer += Time.deltaTime;
    //        transform.position = Vector3.Lerp(startPosition, endPosition, interpolationTimer / stepTime);

    //        yield return null;
    //    }
    //    InputManager.Instance.AllowPlayerInput = true;
    //}
    //IEnumerator RotatePlayer(bool right)
    //{
    //    InputManager.Instance.AllowPlayerInput = false;
    //    Quaternion startRotation = transform.localRotation;
    //    Quaternion endRotation;
    //    if (right)
    //    {
    //        endRotation = transform.localRotation * Quaternion.Euler(0, 90, 0);
    //    }
    //    else
    //    {
    //        endRotation = transform.localRotation * Quaternion.Euler(0, -90, 0);
    //    }
    //    float interpolationTimer = 0;
    //    while (interpolationTimer <= rotationTime)
    //    {
    //        interpolationTimer += Time.deltaTime;
    //        transform.rotation = Quaternion.Lerp(startRotation, endRotation, interpolationTimer / rotationTime);
    //        yield return null;
    //    }
    //    InputManager.Instance.AllowPlayerInput = true;
    //}
}
