using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarterPnlController : MonoBehaviour {
    public List<Transform> CardTrans;
    public GameObject PnlOpts;
    public Button BtnConfirm;
    private List<CardBase> _toPutBack = new();

    private void Awake() {
        foreach (Transform child in PnlOpts.transform) {
            CardTrans.Add(child);
        }

        BtnConfirm.onClick.AddListener(() => {
            foreach (var card in CardTrans) {
                if (card.GetComponent<StarterOptionController>().IsCancelled) {
                    _toPutBack.Add(card.GetComponent<BattleCardManager>().Card);
                }
            }
            BattleControl.you.Deck.DrawCards(_toPutBack.Count);
            foreach (var card in _toPutBack) {
                BattleControl.you.Deck.AddCardToDeck(Random.Range(0, BattleControl.you.Deck.Deck.Count), card);
            }
        });

    }

}
