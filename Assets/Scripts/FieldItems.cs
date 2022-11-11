using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FieldItems : MonoBehaviourPunCallbacks
{
    public Item item;
    public MeshFilter meshFilter;
    public MeshCollider meshCollider;
    ItemDatabase _itemDatabase;

    private void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        meshFilter = GetComponent<MeshFilter>();
        _itemDatabase = FindObjectOfType<ItemDatabase>();
    }
    
    public void SetItem()
    {
        int rand = Random.Range(0, 3);
        photonView.RPC(nameof(SetItemRPC), RpcTarget.AllBuffered, rand);
    }
    
    [PunRPC]
    void SetItemRPC(int rand)
    {
        Item newItem = _itemDatabase.itemDB[rand];
        meshFilter.mesh = newItem.itemmesh;
        meshCollider.sharedMesh = newItem.itemmesh;

        item.itemName = newItem.itemName;
        item.itemmesh = newItem.itemmesh;
        item.itemType = newItem.itemType;
        item.itemImage2 = newItem.itemImage2;
        item.Image = newItem.Image;
        item.efts = newItem.efts;
        item.ItemList = newItem.ItemList;
        item.attackType = newItem.attackType;
    }

    public void DestroyItem()
    {
        photonView.RPC(nameof(DestroyItemRPC), RpcTarget.AllBuffered);
    }
    
    [PunRPC]
    void DestroyItemRPC()
    {
        Destroy(gameObject);
    }
    
    public Item GetItem()
    {
        return item;
    }
    
}
