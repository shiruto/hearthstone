using UnityEngine;

public class ManaLogic {
    // TODO: overload Crystals
    public PlayerLogic owner;
    private int _manas = 0;
    public int Manas {
        get => _manas;
        set {
            if (value > crystalNum) {
                _manas = crystalNum;
            }
            else if (_manas + value < 0) {
                Debug.Log("Insufficient mana");
            }
            else {
                _manas = value;
            }
            EventManager.Invoke(EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.ManaVisualUpdate));
        }
    }

    private int crystalNum = 0;
    public int CurCrystals {
        get => crystalNum;
        set {
            if (crystalNum == MaxCrystalNum && value > 0) {
                // TODO:
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
        CurCrystals = 0;
    }

    public void GainEmptyCrystal(int EmptyCrystalNum) { // 获得空水晶
        CurCrystals += EmptyCrystalNum;
        Manas -= EmptyCrystalNum;
    }

    public void ChangeMaxCrystalNum(int newMaxCrystalNum) { // 改变水晶上限
        MaxCrystalNum = newMaxCrystalNum;
    }

    public void ManaReset() { // 充满所有水晶
        Manas = CurCrystals;
    }
}
