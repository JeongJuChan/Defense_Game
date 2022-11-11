using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class RPCTest : MonoBehaviourPunCallbacks
{
    // TODO 지우기
    void Update()
    {
        if (photonView.IsMine && Input.GetMouseButtonDown(0))
        {
            RpcSendMessage("Hello, World!");
            photonView.RPC(nameof(RpcSendMessage), RpcTarget.All, "Hello, World");
        }
    }

    [PunRPC]
    void RpcSendMessage(string message)
    {
        Debug.Log(message);
    }
}
