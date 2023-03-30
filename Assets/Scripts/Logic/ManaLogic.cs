using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ManaLogic : MonoBehaviour {
    // Start is called before the first frame update
    private int manas = 0;
    public int Manas {
        get => manas;
        set {
            if (manas + value > crystalNum) {
                manas += value;
            }
            else if (manas + value < 0) {
                Debug.Log("Insufficient mana");
                manas = 0;
            }
            else {
                manas = crystalNum;
            }
            UpdateMana();
        }
    }
    private int crystalNum = 0;
    public int CurCrystals {
        get => crystalNum;
        set {
            if (crystalNum == MaxCrystalNum) {
                // TODO give a spell card with 'draw a card' effect
            }
            else if (crystalNum + value > MaxCrystalNum) {
                crystalNum = MaxCrystalNum;
            }
            else {
                crystalNum += value;
            }
        }
    }
    private int MaxCrystalNum = 10;
    readonly Crystal[] Crystals = new Crystal[10];
    TextMeshProUGUI TxtMana;

    private ManaLogic() {
        crystalNum = 0;
        // ManaReset();
    }
    private static ManaLogic yourMana;
    private static ManaLogic opponentMana;
    public static ManaLogic YourMana() {
        if (yourMana == null) {
            yourMana = new();
        }
        return yourMana;
    }
    public static ManaLogic OpponentMana() {
        if (opponentMana == null) {
            opponentMana = new();
        }
        return opponentMana;
    }

    private void Start() {
        Transform[] myTransforms = GameObject.Find("PnlManaGraph").GetComponentsInChildren<Transform>();
        for (int i = 0; i < 10; i++) {
            Crystals[i] = myTransforms[i + 1].GetComponent<Crystal>();
        }
        TxtMana = transform.Find("TxtMana").GetComponent<TextMeshProUGUI>();
        TxtMana.text = "0/0";
    }

    private void UpdateMana() {
        TxtMana.text = manas + "/" + crystalNum;
    }

    public void GainEmptyCrystal(int EmptyCrystalNum) { // 获得空水晶
        CurCrystals += EmptyCrystalNum;
        Manas -= EmptyCrystalNum;
        UpdateMana();
    }

    public void ChangeMaxCrystalNum(int newMaxCrystalNum) { // 改变水晶上限
        MaxCrystalNum = newMaxCrystalNum;
        UpdateMana();
    }

    public void ManaReset() { // 充满所有水晶
        manas = crystalNum;
        UpdateMana();
    }
}
