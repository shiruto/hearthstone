using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldVisual : MonoBehaviour {
    public FieldLogic Field;
    private List<Transform> MinionsOnField = new(7);

    public GameObject PfbMinion;

    private void Awake() {
        EventManager.AddListener(MinionEvent.AfterMinionSummon, CreateMinionOnField);
        EventManager.AddListener(MinionEvent.AfterMinionDie, MinionDie);
        // foreach (Transform child in transform) {
        //     MinionsOnField.Add(child);
        // }
        MinionsOnField = transform.Cast<Transform>().ToList(); // this really work?? TODO: remove it if not working right
    }

    public void CreateMinionOnField(BaseEventArgs _eventData) {
        MinionEventArgs _event = _eventData as MinionEventArgs;
        int position = _event.MinionSummonPos;
        MinionLogic ML = _event.minion;
        var M = Instantiate(PfbMinion, transform);
        M.transform.localPosition = new(0, 0, 0);
        M.GetComponent<MinionManager>().ML = ML;
        M.GetComponent<MinionManager>().ReadFromMinionLogic();
        if (MinionsOnField.Count == 0) {
            MinionsOnField.Add(M.transform);
        }
        else MinionsOnField.Insert(position, M.transform);
        AlignTheField();
    }

    public void MinionDie(BaseEventArgs _eventData) {
        MinionLogic minion = (_eventData as MinionEventArgs).minion;
        MinionsOnField.Remove(MinionsOnField.First((Transform a) => a.GetComponent<MinionManager>().ML == minion));
        AlignTheField();
    }

    public void AlignTheField() {
        int startPosX = (MinionsOnField.Count * 150 + 100) / 2;
        for (int i = 0; i < MinionsOnField.Count; i++) {
            startPosX += 150;
            MinionsOnField[i].localPosition = new(i, 0, 0);
        }
    }

}
