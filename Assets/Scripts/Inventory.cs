using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemList
{
    Sword,
    Dagger,
    HpPotion
}

public class Inventory : MonoBehaviour
{
    public Dictionary<ItemList, GameObject> ItemDict { get; private set; } 
        = new Dictionary<ItemList, GameObject>();

   /* #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion*/

    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();
    int ItemLength;
    private int slotCnt;
    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Init();
        SlotCnt = 4;
    }

    

    void Init()
    {
        string[] itemNames = Enum.GetNames(typeof(ItemList));
        Array arr = Enum.GetValues(typeof(ItemList));
        
        for (int i = 0; i < arr.Length; i++)
        {
            ItemList item = (ItemList)arr.GetValue(i);
             ItemDict.Add(item, GetGameObejct(itemNames[i]));
             ItemDict[item].SetActive(false);
        }
    }

    GameObject GetGameObejct(string name)
    {
        foreach (Transform trans in transform.GetComponentsInChildren<Transform>())
        {
            if (trans.name == name)
                return trans.gameObject;    
        }
        return null;
    }

    public bool AddItem(Item _item)
    {
        if(items.Count < SlotCnt)
        {
            items.Add(_item);
            if(onChangeItem != null)
            onChangeItem.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem.Invoke();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("FieldItem"))
        {
           FieldItems fieldItems = collision.GetComponent<FieldItems>();
            if (AddItem(fieldItems.GetItem()))
            {
                fieldItems.DestroyItem();
            }
        }
    }

}
