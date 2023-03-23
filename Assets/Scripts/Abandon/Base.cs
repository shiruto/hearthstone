using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
    public virtual void Use(CardAsset CA) {
        Debug.Log("Undefined Use func");
    }

    protected bool TryUse(CardAsset CA) {

        return false;
    }
    protected virtual void SummonMinion(CardAsset Minion) {
        Debug.Log("Undefined SummonMinion func");
    }

    protected virtual void DoDamage(Transform Target) {
        Debug.Log("Undefined DoDamage func");
    }

    protected virtual void ChangeStatus(Transform Target, int HealthChange, int AttckChange) {
        Debug.Log("Undefined ChangeStatus func");
    }


    protected virtual void UseOtherCard(params CardAsset[] CAs) {
        foreach (CardAsset Card in CAs) {
            // OnCardUseHandler();
        }
    }

    protected virtual void GetArmor(Transform Target, int Armor) {
        Debug.Log("Undefined GetArmor func");
    }

}
