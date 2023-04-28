using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldVisual : MonoBehaviour {
    public FieldLogic Field;
    public List<Transform> MinionsOnField = new(7);

    private void Awake() {
        EventManager.AddListener(EmptyParaEvent.FieldVisualUpdate, FieldVisualUpdateHandler);
        MinionsOnField = transform.Cast<Transform>().ToList(); // this really work?? yes it worked perfectly
    }

    public void FieldVisualUpdateHandler(BaseEventArgs e) {
        for (int i = 0; i < 7; i++) {
            MinionsOnField[i].gameObject.SetActive(i < Field.Minions.Count);
            if (i < Field.Minions.Count) {
                MinionsOnField[i].GetComponent<DraggableMinion>().Minion = MinionsOnField[i].GetComponent<MinionViewController>().ML = Field.Minions[i];
                MinionsOnField[i].GetComponent<MinionViewController>().ReadFromMinionLogic();
            }
        }
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
