using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MonsterSpawner"))
        {
            Debug.Log("Gamw to xristo");
            PhotonObjects.Instance.SpawnMonster();
            Destroy(other.gameObject, 1f);
        }
        if (other.CompareTag("Exit")) 
        {
            PhotonObjects.Instance.photonView.RPC("Win", RpcTarget.All);
        }
        if (other.CompareTag("Enemy")) 
        {
            PhotonObjects.Instance.JUMPSCARE();
            Destroy(other.gameObject);
        }
    }
}
