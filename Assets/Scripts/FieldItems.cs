using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;
    public MeshFilter meshFilter;
    public MeshCollider meshcollider;


    private void Awake()
    {
        meshcollider = GetComponent<MeshCollider>();
        meshFilter = GetComponent<MeshFilter>();
    }
    public void SetItem(Item _item)
    {
        meshFilter.mesh = _item.itemmesh;
        meshcollider.sharedMesh = _item.itemmesh;


        item.itemName = _item.itemName;
        item.itemmesh = _item.itemmesh;
        item.itemType = _item.itemType;
        item.itemImage2 = _item.itemImage2;
        item.Image = _item.Image;
        item.efts = _item.efts;
        item.ItemList = _item.ItemList;
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroyItem() {
        Destroy(gameObject);
            }
}
