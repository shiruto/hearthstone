using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaLogic : MonoBehaviour {
    // Start is called before the first frame update
    private int ManaNum;
    private int CurMaxCrystalNum;
    private int MaxCrystalNum = 10;
    readonly Crystal[] Crystals = new Crystal[10];
    TextMeshProUGUI TxtMana;

    private ManaLogic() {

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
        TxtMana = GameObject.Find("TxtMana").GetComponent<TextMeshProUGUI>();
        TxtMana.text = "0/0";
    }

    private void UpdateMana() {
        TxtMana.text = ManaNum + "/" + CurMaxCrystalNum;
    }

    public void RestoreMana(int RestoreManaNum) { //复原水晶
        int newAvailableCrystalNum = Mathf.Min(RestoreManaNum, CurMaxCrystalNum - ManaNum);
        for (; ManaNum < newAvailableCrystalNum; ManaNum++) {
            Crystals[ManaNum].Charge();
        }
        UpdateMana();
    }

    public void CostMana(int CostManaNum) { // 消耗水晶
        if (CostManaNum - ManaNum < 0) { // 检查法力值是否足够
            Debug.Log("inSuffcient Mana");
            return;
        }
        int newManaNum = ManaNum - CostManaNum;
        for (; ManaNum > newManaNum; ManaNum--) {
            Crystals[ManaNum - 1].Consume();
        }
        UpdateMana();
    }

    public void GainCrystal(int GainCrystalNum) { // 获得水晶
        int newAvailableCrystalNum = Mathf.Min(MaxCrystalNum, ManaNum + GainCrystalNum);
        int newCurMaxCrystalNum = Mathf.Min(MaxCrystalNum, CurMaxCrystalNum + GainCrystalNum);
        for (; CurMaxCrystalNum < newCurMaxCrystalNum; CurMaxCrystalNum++) {
            Crystals[CurMaxCrystalNum].Activate();
        }
        for (; ManaNum < newAvailableCrystalNum; ManaNum++) {
            Crystals[ManaNum].Charge();
        }
        UpdateMana();
    }

    public void GainEmptyCrystal(int EmptyCrystalNum) { // 获得空水晶
        int newCurMaxCrystalNum = Mathf.Min(MaxCrystalNum, CurMaxCrystalNum + EmptyCrystalNum);
        for (; CurMaxCrystalNum < newCurMaxCrystalNum; CurMaxCrystalNum++) {
            Crystals[CurMaxCrystalNum].Activate();
        }
        UpdateMana();
    }

    public void DestroyCrystal(int DestroyCrystalNum) { // 摧毁水晶
        int newCurMaxCrystalNum = Mathf.Max(0, CurMaxCrystalNum - DestroyCrystalNum);
        for (; CurMaxCrystalNum > newCurMaxCrystalNum; newCurMaxCrystalNum--) {
            Crystals[CurMaxCrystalNum - 1].Destroy();
        }
        UpdateMana();
    }

    public void ChangeMaxCrystalNum(int newMaxCrystalNum) { // 改变水晶上限
        MaxCrystalNum = newMaxCrystalNum;
    }

    public void ManaReset() { // 充满所有水晶
        ManaNum = CurMaxCrystalNum;
    }
}
