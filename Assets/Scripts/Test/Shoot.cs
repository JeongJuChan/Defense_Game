using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Shoot : MonoBehaviourPunCallbacks
{
    public Bullet bulletPrefab;
    public GameObject shootPos;
    int bulletNextID = 0;

    void OnShoot()
    {
        if (!photonView.IsMine)
            return;
        Vector3 dir = transform.forward;
        // Fire(dir);
        photonView.RPC(nameof(Fire), RpcTarget.All, bulletNextID++, dir);
    }
    
    [PunRPC]
    void Fire(int id, Vector3 dir)
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.Init(id, photonView.OwnerActorNr, dir);
        bullet.transform.position = shootPos.transform.position;
    }
}
