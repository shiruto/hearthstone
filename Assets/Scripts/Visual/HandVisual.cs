using System.Collections.Generic;
using UnityEngine;

public class HandVisual : MonoBehaviour {
    public HandLogic Hand;
    public List<Transform> CardTrans = new();
    public GameObject PfbBattleCard;
    Vector3 pivot = Vector3.zero;

    private void Awake() {
        foreach (Transform child in transform) {
            CardTrans.Add(child);
            child.gameObject.SetActive(false);
        }
        EventManager.AddListener(EmptyParaEvent.HandVisualUpdate, VisualUpdate);
    }

    private void VisualUpdate(BaseEventArgs e) {
        // Debug.Log("Hand VisualUpdate");
        if (CardTrans.Count > Hand.Hands.Count) {
            for (int i = 0; i < CardTrans.Count; i++) {
                if (i < Hand.Hands.Count) {
                    CardTrans[i].gameObject.SetActive(true);
                    CardTrans[i].GetComponent<BattleCardViewController>().Card = Hand.Hands[i];
                    CardTrans[i].GetComponent<BattleCardViewController>().ReadFromAsset();
                }
                else {
                    CardTrans[i].gameObject.SetActive(false);
                }
            }
        }
        else {
            for (int i = 0; i < Hand.Hands.Count; i++) {
                if (i < CardTrans.Count) {
                    CardTrans[i].gameObject.SetActive(true);
                }
                else {
                    CardTrans.Add(Instantiate(PfbBattleCard, transform).transform); // TODO: ?
                }
                CardTrans[i].GetComponent<BattleCardViewController>().Card = Hand.Hands[i];
                CardTrans[i].GetComponent<BattleCardViewController>().ReadFromAsset();
            }
        }
        AlignTheCards();
    }

    private void AlignTheCards() {
        int Dis = 76;
        if (Hand.Hands.Count > 8) { // TODO:
            Debug.Log(Hand.Hands.Count);
            for (int i = 0; i < Hand.Hands.Count; i++) {
                CardTrans[i].name = "card" + (i + 1);
            }
        }
        else if (Hand.Hands.Count > 1) {
            int PanelWidth = 114 + (Hand.Hands.Count - 2) * 76;
            for (int i = 0; i < Hand.Hands.Count; i++) {
                pivot.x = -PanelWidth / 2 + 57 + i * Dis;
                CardTrans[i].localPosition = pivot;
                CardTrans[i].name = "card" + (i + 1);
                CardTrans[i].GetComponent<BoxCollider>().center = new(0, 0, -i);
            }
        }
        else if (Hand.Hands.Count == 1) {
            CardTrans[0].localPosition = new(0, 0, 0);
            CardTrans[0].name = "card1";
        }
    }
}
