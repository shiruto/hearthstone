using UnityEngine;

public class BattleControl : MonoBehaviour {
    private void Awake() {
        Draggable.OnCardUse += OnCardUseHandler;
    }

    private void SummonMinion(CardAsset Minion) {

    }

    private void DoDamage(Transform Target) {

    }

    private void ChangeStatus(Transform Target, int HealthChange, int AttckChange) {

    }

    private void Heal(Transform Target, int HealingAmount) {

    }

    private void UseOtherCard(params CardAsset[] CAs) {
        foreach(CardAsset Card in CAs) {
            // OnCardUseHandler();
        }
    }

    private void GetArmor(Transform Target, int Armor) {
        
    }

    private void OnCardUseHandler(Transform UsedCard) {
        CardAsset CA = UsedCard.GetComponent<CardManager>().cardAsset;
        if(CA.MaxHealth > 0) {
            SummonMinion(CA);
        }
    }
}