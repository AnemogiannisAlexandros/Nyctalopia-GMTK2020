using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class PhotonObjects : MonoBehaviourPunCallbacks
{

    public static PhotonObjects Instance;
    public GameObject monster;
    public GameObject jumpScareModel;
    public float forwardOffset;
    public GameObject[] playerObjects;

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
    public void Start()
    {
        if (!PhotonNetwork.IsMasterClient) 
        {
            PhotonNetwork.Instantiate("Player", new Vector3(26.131f, 0.057978f, 21.847f), Quaternion.Euler(0, -123, 0));
        }
    }
    [PunRPC]
    public void SpawnMonster()
    {
        monster.SetActive(true);
    }
    [PunRPC]
    public void Win() 
    {
        PhotonNetwork.LoadLevel(3);
    }
    public void JUMPSCARE() 
    {
            StartCoroutine(jumpscareSequence());
    }
    IEnumerator jumpscareSequence() 
    {
        if (PhotonNetwork.IsMasterClient)
        {
            InputManager.Instance.AllowCameraInput = false;
            InputManager.Instance.AllowPlayerInput = false;
            AudioManager.Instance.photonView.RPC("PlayOnServerFX", RpcTarget.All, 7);
            yield return new WaitForSeconds(5);
            Transform spawnPosition = Camera.main.transform.parent.GetComponent<CameraRotate>().jumpscareSpawn;
            Instantiate(jumpScareModel, spawnPosition.position, Quaternion.Euler(-15, 180, 0), spawnPosition);
            yield return new WaitForSeconds(8);
            PhotonNetwork.Disconnect();
            Application.Quit();
        }
        else 
        {
            InputManager.Instance.AllowCameraInput = false;
            InputManager.Instance.AllowPlayerInput = false;
            AudioManager.Instance.photonView.RPC("PlayOnServerFX", RpcTarget.All, 7);
            yield return new WaitForSeconds(5);
            Transform spawnPosition = Camera.main.transform.parent.Find("JumpScareSpawn").transform;
            GameObject go = Instantiate(jumpScareModel, spawnPosition.position, Quaternion.Euler(0,180,0),spawnPosition);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.Euler(0, 0, 0);
            yield return new WaitForSeconds(8);
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(0);
            Application.Quit();
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        SceneManager.LoadScene(0);
    }
}
