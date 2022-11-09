using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Slot : MonoBehaviour, IPointerUpHandler
{

    PlayerController player_status;

    [SerializeField]
    Inventory inventory;
    Animator _animator;
    
    public int slotnum;
    public Item item;
    public Image itemIcon;
    public GameObject Player;

    private void Start()
    {
        player_status = GetComponentInParent<PlayerController>();
        _animator = player_status.GetComponentInChildren<Animator>();
    }


    //���� ������Ʈ (��ü �߰�)
    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.Image;
        itemIcon.gameObject.SetActive(true);
    }

    //���� �ʱ�ȭ (��ü ����)
    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }


    public void OnEquip()
    {
        // UI적으로 보여줄 스트립트
        GetComponentInParent<Inventory>().ItemDict[item.ItemList].SetActive(true);
    }

    public void OnUpEquip()
    {
        // UI적으로 보여줄 스트립트
        GetComponentInParent<Inventory>().ItemDict[item.ItemList].SetActive(false);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        bool isUse = item.Use();
        if (isUse)
        {
            //HP포션 사용
            if (item.itemType == ItemType.Consumables)
            {
                Debug.Log("Hp를 회복하였습니다!");
                GetComponentInParent<Inventory>().ItemDict[item.ItemList].SetActive(true);
                inventory.RemoveItem(slotnum);
                return;
            }

            // 장비를 장착하지 않고있을때
            if (item.itemType == ItemType.Equipment)
            {
                bool isEquip = player_status.GetIsEquip();
                if (!isEquip)
                {
                    player_status.SetIsEquip(true);
                    //Debug.Log("Sword를 장착하였습니다!");
                    // UI적으로 보여줄 스트립트
                    player_status.WeaponSet(item ,true);
                    // 실질적으로 데미지를 올려줄 스크립트 
                    player_status._damage += item.efts[0].value;
                    Debug.Log(item.attackType);
                    player_status.SetAttackType(item.attackType);
                }
                else
                {
                    player_status.SetIsEquip(false);
                    //Debug.Log("Sword를 장착 해제하였습니다!");
                    player_status.WeaponSet(item ,false);
                    // 실질적으로 데미지를 내려줄 스크립트 
                    player_status._damage -= item.efts[0].value;
                    player_status.SetAttackType(AttackType.None);
                }
            }
        }
    }
}


