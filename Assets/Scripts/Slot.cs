using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using Photon.Pun;

public class Slot : MonoBehaviourPunCallbacks, IPointerUpHandler
{
    public int slotNum;
    public Item item;
    public Image itemIcon;
    public GameObject player;

    [SerializeField]
    Inventory inventory;
    
    Animator _animator;
    PlayerController _playerStatus;

    private void Start()
    {
        _playerStatus = GetComponentInParent<PlayerController>();
        _animator = _playerStatus.GetComponentInChildren<Animator>();
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
                photonView.RPC(nameof(WeaponSetRPC), RpcTarget.AllBuffered, false);
                inventory.RemoveItem(slotNum);
                return;
            }

            // TODO : 리팩토링
            // 장비를 장착하지 않고있을때
            if (item.itemType == ItemType.Equipment)
            {
                bool isEquip = _playerStatus.GetIsEquip();
                if (!isEquip)
                {
                    _playerStatus.SetIsEquip(true);
                    //Debug.Log("Sword를 장착하였습니다!");
                    // UI적으로 보여줄 스트립트
                    // 실질적으로 데미지를 올려줄 스크립트
                    photonView.RPC(nameof(WeaponSetRPC), RpcTarget.AllBuffered, true);
                    _playerStatus.SetDamage(item.efts[0].value);
                    _playerStatus.SetAttackType(item.attackType);
                }
                else
                {
                    _playerStatus.SetIsEquip(false);
                    //Debug.Log("Sword를 장착 해제하였습니다!");
                    photonView.RPC(nameof(WeaponSetRPC), RpcTarget.AllBuffered, false);
                    // 실질적으로 데미지를 내려줄 스크립트
                    _playerStatus.SetDamage(-item.efts[0].value);
                    _playerStatus.SetAttackType(AttackType.None);
                }
            }
        }
    }
    
    [PunRPC]
    void WeaponSetRPC(bool isActive)
    {
        _playerStatus.WeaponSet(item ,isActive);
    }
    
    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.Image;
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }
}


