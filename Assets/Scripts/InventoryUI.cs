using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviourPunCallbacks
{
    public GameObject inventoryPanel;
    public Slot[] slots;
    public Transform slotHolder;
    
    [SerializeField] Inventory inven;

    bool _activeInventory = false;

    void Start()
    {
        slots = slotHolder.GetComponentsInChildren<Slot>();

        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;

        inventoryPanel.SetActive(_activeInventory);
    }
    
    void OnInventory()
    {
        if (!photonView.IsMine)
            return;
        Debug.Log("?¥ê??? ");
        _activeInventory = !_activeInventory;
        inventoryPanel.SetActive(_activeInventory);
    }

    public void AddSlot()
    {
        Debug.Log(" ????");
        inven.SlotCount++;
    }

    
    void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;
            
            if (i < inven.SlotCount)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = true;
        }
    }
    
    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}
