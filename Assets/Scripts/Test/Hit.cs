using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

// 맞는 측에서 계산
public class Hit : MonoBehaviourPunCallbacks
{
    void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
            return;
        if (other.TryGetComponent<Bullet>(out var bullet))
        {
            if (bullet.OwnerID != PhotonNetwork.LocalPlayer.ActorNumber)
            {
                photonView.RPC(nameof(HitBullet), RpcTarget.All, bullet.ID, bullet.OwnerID);
            }
        }
    }

    [PunRPC]
    void HitBullet(int id, int ownerId)
    {
        var bullets = FindObjectsOfType<Bullet>();
        foreach (var bullet in bullets)
        {
            if (bullet.Equals(id, ownerId))
            {
                Destroy(bullet.gameObject);
                break;
            }
        }
    }
}
