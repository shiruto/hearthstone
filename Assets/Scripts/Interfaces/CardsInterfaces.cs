using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsInterfaces {
    public interface ICard {
        public void Use();
    }
    public interface ISummon {
        public void SummonMinion(CardAsset CA);
    }

    public interface IDoDamage {
        public void DoDamage(Transform Target, int DoDamageNum);
    }
    public interface IChangeStatus {
        public void ChangeStatus(Transform Target, int HealthChange, int AttckChange);
    }

    public interface IUseOtherCard {
        public void UseOtherCard(params CardAsset[] CAs);
    }

    public interface IGetArmor {
        public void GetArmor(Transform Target, int Armor);
    }
}
