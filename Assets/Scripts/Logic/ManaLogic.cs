using UnityEngine;

public class ManaLogic {
    public int overloadCrystal = 0;
    public int aboutToOverload; // overload next turn
    public int tempCrystal = 0;
    public PlayerLogic owner;
    private int _manas = 0;
    public int Manas {
        get => _manas;
        set {
            if (value > crystalNum) {
                _manas = crystalNum;
            }
            else if (value < 0) {
                Debug.Log("Insufficient mana");
                _manas = 0;
            }
            else {
                _manas = value;
            }
            EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.ManaVisualUpdate).Invoke();
        }
    }

    private int crystalNum = 0;
    public int CurCrystals {
        get => crystalNum;
        set {
            if (crystalNum == MaxCrystalNum && value > 0) {
                // TODO: debug it
                owner.Hand.GetCard(-1, new ExcessMana(Resources.Load<CardAsset>("ScriptableObject/UncollectableCard/ExcessMana.asset")));
            }
            else if (value > MaxCrystalNum) {
                crystalNum = MaxCrystalNum;
            }
            else {
                crystalNum = value;
            }
            EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.ManaVisualUpdate).Invoke();
        }
    }

    private int MaxCrystalNum = 10;

    public ManaLogic() {
        CurCrystals = 0;
        EventManager.AddListener(TurnEvent.OnTurnStart, OnTurnStartHandler);
        EventManager.AddListener(TurnEvent.OnTurnEnd, OnTurnEndHandler);
        EventManager.AddListener(ManaEvent.OnEmptyCrystalGet, (BaseEventArgs e) => {
            if (e.Player == owner) CurCrystals += (e as ManaEventArgs).CrystalNum;
        });
        EventManager.AddListener(ManaEvent.OnManaRecover, (BaseEventArgs e) => {
            if (e.Player == owner) Manas += (e as ManaEventArgs).CrystalNum;
        });
        EventManager.AddListener(ManaEvent.OnPermanentCrystalGet, GetCrystal);
        EventManager.AddListener(ManaEvent.OnTemporaryCrystalGet, GetTempCrystal);
        EventManager.AddListener(CardEvent.OnCardUse, (BaseEventArgs e) => {
            if (e.Player == owner) Manas -= (e as CardEventArgs).Card.ManaCost;
        });
        EventManager.AddListener(ManaEvent.OnCrystalOverload, (BaseEventArgs e) => {
            if (e.Player == owner) aboutToOverload += (e as ManaEventArgs).CrystalNum;
        });
    }

    private void OnTurnStartHandler(BaseEventArgs e) {
        TurnEventArgs evt = e as TurnEventArgs;
        if (evt.Player == owner) {
            overloadCrystal = aboutToOverload;
            aboutToOverload = 0;
            if (crystalNum < 10) crystalNum++;
            ManaReset();
            Manas -= overloadCrystal;
        }
    }

    private void OnTurnEndHandler(BaseEventArgs e) {
        TurnEventArgs evt = e as TurnEventArgs;
        if (evt.Player == owner) {
            overloadCrystal = 0;
            CurCrystals -= tempCrystal;
            Manas -= tempCrystal;
            tempCrystal = 0;
        }
    }

    public void GetCrystal(BaseEventArgs e) {
        if (e.Player == owner) {
            int CrystalNum = (e as ManaEventArgs).CrystalNum;
            CurCrystals += CrystalNum;
            Manas += CrystalNum;
        }
    }

    public void GetTempCrystal(BaseEventArgs e) {
        if (e.Player == owner) {
            int tempCrystalNum = (e as ManaEventArgs).CrystalNum;
            CurCrystals += tempCrystalNum;
            Manas += tempCrystalNum;
            tempCrystal += tempCrystalNum;
        }
    }

    public void ChangeMaxCrystalNum(int newMaxCrystalNum) { // 改变水晶上限
        MaxCrystalNum = newMaxCrystalNum;
    }

    public void ManaReset() { // 充满所有水晶
        Manas = CurCrystals;
    }
}
