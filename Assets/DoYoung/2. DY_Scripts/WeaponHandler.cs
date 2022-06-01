using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public enum State
    {
        Equip, Unequip
    }

    public Transform weapon;
    public Transform weaponHandle;
    public Transform weaponeRest;

    public void ResetWeapon(State state)
    {
        if (state == State.Equip)
        {
            print("adfsf");
            weapon.SetParent(weaponHandle, true);
            //default : true -> 개체가 이전과 동일한 월드 위치, 각도, 스케일을 유지하도록 부모오브젝트와 관계에 의해 상대적으로 수정된다고 한다.
        }
        else
        {
            weapon.SetParent(weaponeRest, true);
        }

        weapon.localRotation = Quaternion.identity; //회전 (0, 0, 0)
        weapon.localPosition = Vector3.zero;
    }
}
