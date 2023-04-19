using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarterPnlController : MonoBehaviour {
    public List<Transform> CardTrans;
    public GameObject PnlOpts;
    public Button BtnConfirm;

    private void Awake() {
        foreach (Transform child in PnlOpts.transform) {
            CardTrans.Add(child);
        }

        BtnConfirm.onClick.AddListener(() => {
            foreach (var card in CardTrans) {
                if (card.GetComponent<StarterOptionController>().IsCancelled) {
                    Debug.Log("StarterPnl Draw");
                    BattleControl.you.Deck.DrawCards(1);
                    BattleControl.you.Deck.BackToDeck(card.GetComponent<BattleCardViewController>().Card);
                    Debug.Log("discard a card named: " + card.GetComponent<BattleCardViewController>().Card);
                }
                else {
                    BattleControl.you.Hand.GetCard(-1, card.GetComponent<BattleCardViewController>().Card);
                    Debug.Log("put a card into hand, which named: " + card.GetComponent<BattleCardViewController>().Card);
                }
            }
            BattleControl.Instance.AnotherPlayer.Deck.DrawCards(1);
            BattleControl.Instance.AnotherPlayer.Hand.GetCard(-1, new Coin(Resources.Load<CardAsset>("ScriptableObject/UnCollectableCard/Coin")));
            BattleControl.Instance.ActivePlayer.OnTurnStart();
            gameObject.SetActive(false);
        });
        gameObject.SetActive(false);
    }

}
