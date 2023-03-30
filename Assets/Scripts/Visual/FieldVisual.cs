using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldVisual : MonoBehaviour {
    BattleField Field;
    List<MinionLogic> MinionLogics;
    List<Transform> MinionsOnField = new(7);
    public GameObject PfbMinion;
    private void Awake() {
        Field = GetComponent<BattleField>();
        MinionLogics = Field.GetMinions();
        BattleField.OnMinionSummon += CreateMinionOnField;
    }
    public void CreateMinionOnField(int position, MinionLogic ML) {
        var M = Instantiate(PfbMinion, transform);
        M.transform.localPosition = new(0, 0, 0);
        M.GetComponent<MinionManager>().ML = ML;
        Debug.Log($"Minion just Summoned has {ML.Attack} attacks");
        M.GetComponent<MinionManager>().ReadFromMinionLogic();
    }
}
