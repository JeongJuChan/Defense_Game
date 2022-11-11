using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public enum ItemList
{
    Sword,
    Dagger,
    HpPotion
}

public class Inventory : MonoBehaviourPunCallbacks
{
    public Dictionary<ItemList, GameObject> ItemDict { get; private set; } 
        = new Dictionary<ItemList, GameObject>();

    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();
    
    
    int _itemLength;
    int _slotCount;

    void Start()
    {
        Init();
        SlotCount = 4;
    }

    void Init()
    {
        string[] itemNames = Enum.GetNames(typeof(ItemList));
        Array arr = Enum.GetValues(typeof(ItemList));
        
        for (int i = 0; i < arr.Length; i++)
        {
            ItemList item = (ItemList)arr.GetValue(i);
            ItemDict.Add(item, GetGameObject(itemNames[i]));
            ItemDict[item].SetActive(false);
        }
    }
    
    GameObject GetGameObject(string objName)
    {
        foreach (Transform trans in transform.GetComponentsInChildren<Transform>())
        {
            if (trans.name == objName)
                return trans.gameObject;    
        }
        return null;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("FieldItem"))
        {
            if (!photonView.IsMine)
                return;
            FieldItems fieldItems = collision.GetComponent<FieldItems>();
            if (AddItem(fieldItems.GetItem()))
            {
                fieldItems.DestroyItem();
            }
        }
    }

    public bool AddItem(Item item)
    {
        if(items.Count < SlotCount)
        {
            items.Add(item);
            if(onChangeItem != null)
                onChangeItem.Invoke();
            return true;
        }
        return false;
    }

    public void RemoveItem(int index)
    {
        items.RemoveAt(index);
        onChangeItem.Invoke();
    }
    
    public int SlotCount
    {
        get => _slotCount;
        set
        {
            _slotCount = value;
            onSlotCountChange.Invoke(_slotCount);
        }
    }

    
}
