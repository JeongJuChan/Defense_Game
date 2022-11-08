using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;

public class PlayerNick : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform _cameraRoot;
    void Start()
    {
        var nickName = GetComponent<TMP_Text>();
        nickName.text = photonView.Owner.NickName;
        if (photonView.IsMine)
            transform.parent = _cameraRoot;
    }

    void Update()
    {
        
    }
}
