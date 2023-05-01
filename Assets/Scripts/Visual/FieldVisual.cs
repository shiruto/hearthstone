using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldVisual : MonoBehaviour {
    public FieldLogic Field;
    public List<Transform> MinionTrans = new(7);

    private void Awake() {
        EventManager.AddListener(EmptyParaEvent.FieldVisualUpdate, FieldVisualUpdateHandler);
        MinionTrans = transform.Cast<Transform>().ToList(); // this really work?? yes it worked perfectly
    }

    public void FieldVisualUpdateHandler(BaseEventArgs e) {
        for (int i = 0; i < 7; i++) {
            MinionTrans[i].gameObject.SetActive(i < Field.Minions.Count);
            if (i < Field.Minions.Count) {
                MinionTrans[i].GetComponent<DraggableMinion>().Minion = Field.Minions[i];
                MinionTrans[i].GetComponent<MinionViewController>().ML = Field.Minions[i];
                MinionTrans[i].GetComponent<MinionViewController>().ReadFromMinionLogic();
            }
        }
        AlignTheField();
    }

    public void AlignTheField() {
        int centerX = -((Field.Minions.Count - 1) * 20 + Field.Minions.Count * 130) / 2 + 65;
        for (int i = 0; i < Field.Minions.Count; i++) {
            MinionTrans[i].localPosition = new(centerX, 0, 0);
            centerX += 150;
        }
    }

}
