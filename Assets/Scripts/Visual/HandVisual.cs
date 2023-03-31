using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class HandVisual : MonoBehaviour {
    public List<Transform> CardTrans = new();
    public GameObject PfbBattleCard;
    Vector3 pivot = new(0, 0, 0);
    private void Awake() {
        EventManager.AddListener(CardEvent.OnCardGet, OnCardGetHandler);
        EventManager.AddListener(CardEvent.OnCardUse, OnCardUseHandler);
    }
    void Start() {
        foreach (Transform child in transform) {
            CardTrans.Add(child);
        }
        Align();
    }

    private void OnCardGetHandler(BaseEventArgs _eventData) {
        CardEventArgs _event = _eventData as CardEventArgs;
        int position = _event.position;
        CardBase newCard = _event.Card;
        Transform newCardTrans = Instantiate(PfbBattleCard, transform).transform;
        newCardTrans.GetComponent<BattleCardManager>().Card = newCard;
        newCardTrans.GetComponent<BattleCardManager>().ReadFromAsset();
        if (CardTrans.Count == 0) {
            CardTrans.Add(newCardTrans);
        }
        else {
            CardTrans.Insert(position, newCardTrans);
        }
        Align();
    }

    private void OnCardUseHandler(BaseEventArgs eventData) {
        CardBase CardToLose = (eventData as CardEventArgs).Card;
        Transform temp = CardTrans.First((Transform a) => a.GetComponent<BattleCardManager>().Card == CardToLose);
        CardTrans.Remove(temp);
        Destroy(temp.gameObject);
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
        else if (CardTrans.Count == 1) {
            CardTrans[0].localPosition = new(0, 0, -1);
            CardTrans[0].name = "Card1";
        }
    }
}
