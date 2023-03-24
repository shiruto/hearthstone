using System.Collections.Generic;
using UnityEngine;

public class HandLogic : MonoBehaviour {
    List<Transform> CardTrans = new();
    //public static event Action<Transform> OnUse;
    private Vector3 pivot;
    private int MaxHandCard = 10;

    private HandLogic() {

    }
    private static HandLogic yourHand = null;
    private static HandLogic opponentHand = null;

    public static HandLogic YourHand() {
        if (yourHand == null) {
            yourHand = new();
        }
        return yourHand;
    }
    public static HandLogic OpponentHand() {
        if (opponentHand == null) {
            opponentHand = new();
        }
        return opponentHand;
    }

    void Start() {
        foreach (Transform child in transform) {
            CardTrans.Add(child);
        }
        DeckLogic.OnDraw += OnDrawHandler;
        Draggable.OnCardUse += OnUseHandler;
        pivot = Vector3.zero;
        Align();
    }

    private void OnDisable() {
        DeckLogic.OnDraw -= OnDrawHandler;
    }
    private void OnUseHandler(Transform CardTransform) { // 手牌区改变
        char[] CharNum = CardTransform.name.ToCharArray();
        int index = CharNum[^1] - '0';
        Debug.Log("OnUseHandler: " + " index = " + index);
        DiscardCrad(index);
        Align();
    }

    private void OnDrawHandler(CardAsset Card) {
        Transform CardTrans = (GameObject.Instantiate(Resources.Load("Prefabs/PfbCrad")) as GameObject).transform;
        GetCard(CardTrans);
    }

    public void GetCard(Transform newCard) {
        if (CardTrans.Count >= MaxHandCard) {
            Debug.Log("Too Many Cards");
            return;
        }
        CardTrans.Add(newCard);
        Align();
    }

    public void DiscardCrad(int index) {
        index -= 1;
        Debug.Log("Discarding Card :index = " + index + "\tname = " + CardTrans[index].name);
        Destroy(CardTrans[index].gameObject);
        CardTrans.RemoveAt(index);
        Align();
    }

    private void Align() { // 重新排列卡牌 需要实现创建新增卡牌和销毁旧卡牌
        int Dis = 76;
        Debug.Log("Align");
        if (CardTrans.Count > 6) {
            Debug.Log(CardTrans.Count);
            for (int i = 0; i < CardTrans.Count; i++) {
                Debug.Log(CardTrans[i].name);
                CardTrans[i].name = "card" + (i + 1);
            }
        }
        else if (CardTrans.Count > 1) {
            int PanelWidth = 114 + (CardTrans.Count - 2) * 76;
            for (int i = 0; i < CardTrans.Count; i++) {
                pivot.x = -PanelWidth / 2 + 57 + i * Dis;
                CardTrans[i].localPosition = pivot;
                CardTrans[i].name = "card" + (i + 1);
            }
        }
        else {
            CardTrans[0].position = Vector3.zero;
            CardTrans[0].name = "Card1";
        }
    }
}
