using UnityEngine;

public class ManaLogic {
    // TODO: overload Crystals
    private int manas = 0;
    public int Manas {
        get => manas;
        set {
            if (value > crystalNum) {
                manas = crystalNum;
            }
            else if (manas - value < 0) {
                Debug.Log("Insufficient mana");
            }
            else {
                manas = value;
            }
            EventManager.Invoke(EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.ManaVisualUpdate));
        }
    }

    private int crystalNum = 0;
    public int CurCrystals {
        get => crystalNum;
        set {
            if (crystalNum == MaxCrystalNum && value > 0) {
                BattleControl.Instance.ActivePlayer.Hand.GetCard(-1, new ExcessMana(Resources.Load<CardAsset>("ScriptableObject/UnCollectableCard/ExcessMana.asset")));
            }
            else if (value > MaxCrystalNum) {
                crystalNum = MaxCrystalNum;
            }
            else {
                crystalNum = value;
            }
            EventManager.Invoke(EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.ManaVisualUpdate));
        }
    }

    private int MaxCrystalNum = 10;

    public ManaLogic() {
        crystalNum = 0;
        ManaReset();
    }

    public void GainEmptyCrystal(int EmptyCrystalNum) { // 获得空水晶
        CurCrystals += EmptyCrystalNum;
        Manas -= EmptyCrystalNum;
    }

    public void ChangeMaxCrystalNum(int newMaxCrystalNum) { // 改变水晶上限
        MaxCrystalNum = newMaxCrystalNum;
    }

    public void ManaReset() { // 充满所有水晶
        manas = crystalNum;
    }
}
