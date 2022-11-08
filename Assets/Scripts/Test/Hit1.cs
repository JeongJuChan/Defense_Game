using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

// 쏘는 측에서 계산
public class Hit1 : MonoBehaviourPunCallbacks
{
    void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
            return;
        if (other.TryGetComponent<Bullet>(out var bullet))
        {
            if (bullet.OwnerID == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                photonView.RPC(nameof(HitBullet), RpcTarget.All, bullet.ID);
            }
        }
    }

    [PunRPC]
    void HitBullet(int id, PhotonMessageInfo info)
    {
        var bullets = FindObjectsOfType<Bullet>();
        foreach (var bullet in bullets)
        {
            if (bullet.Equals(id, info.Sender.ActorNumber))
            {
                Destroy(bullet.gameObject);
                break;
            }
        }
    }
}
