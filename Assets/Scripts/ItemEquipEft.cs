using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEft/Equipment/Weapon")]
public class ItemEquipEft : ItemEffect
{
    public int DamagePoint = 0;
    public string ItemName;

    private void Awake()
    {
        value = DamagePoint;
    }

    public override bool ExcuteRole()
    {
        Debug.Log("PlayerDamage Add: " + DamagePoint);
        return true;
    }
}
