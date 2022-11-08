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
        player_status.Equipment = "1";
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
            if (item.itemName == "HpPotion")
            {
                Debug.Log("Hp를 회복하였습니다!");
                GetComponentInParent<Inventory>().ItemDict[item.ItemList].SetActive(true);
                inventory.RemoveItem(slotnum);
            }

            // 장비를 장착하고 있을때
            if (player_status.IsEquip == true) {

                if (player_status.Equipment == "Sword")
                {
                    //Sword 사용
                    if (item.itemName == "Sword")
                    {
                        _animator.SetTrigger("UnEquip");
                        Debug.Log("Sword를 장착 해제하였습니다!");
                        player_status.Equipment = "";
                        ;
                        // 실질적으로 데미지를 내려줄 스크립트 
                        player_status.IsEquip = false;
                        player_status._damage = player_status._damage - item.efts[0].value;
                    }
                }

                //Dagger 사용
                if (item.itemName == "Dagger")
                {
                    _animator.SetTrigger("UnEquip");
                    Debug.Log("Dagger를 장착 해제하였습니다!");
                    player_status.Equipment = "";
                    // UI적으로 보여줄 스트립트
                    GetComponentInParent<Inventory>().ItemDict[item.ItemList].SetActive(false);
                    // 실질적으로 데미지를 내려줄 스크립트 
                    player_status.IsEquip = false;
                    player_status._damage = player_status._damage - item.efts[0].value;
                }

            }

            // 장비를 장착하지 않고있을때
            else
            {
                //Sword 사용
                if (item.itemName == "Sword")
                {
                    _animator.SetTrigger("Equip");
                    Debug.Log("Sword를 장착하였습니다!");
                    // UI적으로 보여줄 스트립트
                    player_status.Equipment = "Sword";
                    GetComponentInParent<Inventory>().ItemDict[item.ItemList].SetActive(true);
                    // 실질적으로 데미지를 올려줄 스크립트 
                    player_status.IsEquip = true;
                    player_status._damage = player_status._damage + item.efts[0].value;




                    player_status.IsEquip = true;
                }

                //Dagger 사용
                if (item.itemName == "Dagger")
                {
                    _animator.SetTrigger("Equip");
                    Debug.Log("Dagger를 장착하였습니다!");
                    player_status.Equipment = "Dagger";
                    // UI적으로 보여줄 스트립트
                    GetComponentInParent<Inventory>().ItemDict[item.ItemList].SetActive(true);
                    // 실질적으로 데미지를 올려줄 스크립트 
                    player_status.IsEquip = true;
                    player_status._damage = player_status._damage + item.efts[0].value;
                }
            }
            
        }
    }
}
