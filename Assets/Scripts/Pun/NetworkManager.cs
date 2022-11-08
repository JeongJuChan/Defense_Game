using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI inputText;
    static NetworkManager _instance;

    public static NetworkManager Instance
    {
        get
        {
            if (!_instance)
            {
                GameObject go = new GameObject("@NetworkManager");
                _instance = go.AddComponent<NetworkManager>();
            }
            
            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void ConnectBtn()
    {
       // PhotonNetwork.IsMessageQueueRunning = false;
        SceneManager.LoadScene(1);
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = inputText.text;
        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
        var player = PhotonNetwork.LocalPlayer;
        Debug.Log(player);
    }

    public override void OnJoinedRoom()
    {
        //PhotonNetwork.IsMessageQueueRunning = true;
        var pos = new Vector3(Random.Range(40f, 43f), 6f, Random.Range(66f, 70f));
        PhotonNetwork.Instantiate("Player", pos, Quaternion.identity);

    }

    [ContextMenu("PlayerList")]
    public void PlayerListPrint()
    {
        var players = PhotonNetwork.PlayerList;
        foreach (var VARIABLE in players)
        {
            Debug.Log(VARIABLE); 
        }
    }
}
