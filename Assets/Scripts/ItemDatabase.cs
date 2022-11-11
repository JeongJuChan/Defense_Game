using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    static ItemDatabase _instance;
    
    public List<Item> itemDB = new List<Item>();

    public GameObject fieldItemPrefab;
    public Vector3[] pos;
    
    private void Awake()
    {
        _instance = this;
    }
    
    public void CreateItem()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        for (int i = 0; i < 3; i++)
        {
            GameObject go = PhotonNetwork.Instantiate(Path.Combine("prefabs", "FieldItem"), pos[i], Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem();
        }
    }
}
