using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckVisual : MonoBehaviour {
    public DeckLogic Deck;
    public List<string> decks;
    // private Image ImgDeckStatus;
    public GameObject DeckInfo;
    public TextMeshProUGUI TxtCardSum;
    private DeckStatus CardLeft;

    private void Awake() {
        EventManager.AddListener(EmptyParaEvent.DeckVisualUpdate, UpdateDeckStatus);
        DeckInfo.SetActive(false);
    }

    public void UpdateDeckStatus(BaseEventArgs e) {
        decks = new(Deck?.cardName);
        if (e.Player == Deck.owner)
            switch (Deck.Deck.Count) {
                case >= 60:
                    CardLeft = DeckStatus.Full;
                    break;
                case > 30:
                    CardLeft = DeckStatus.Alot;
                    break;
                case > 10:
                    CardLeft = DeckStatus.Medium;
                    break;
                case > 1:
                    CardLeft = DeckStatus.Less;
                    break;
                case 1:
                    CardLeft = DeckStatus.LastOne;
                    break;
                case 0:
                    CardLeft = DeckStatus.Empty;
                    break;
                default:
                    Debug.Log("Wrong Sum of Cards in the Deck, have " + Deck.Deck.Count + " cards");
                    break;
            }
        // TODO: asscociate img with Deck.Sum
    }

    private void OnMouseEnter() {
        DeckInfo.SetActive(true);
        TxtCardSum.text = "" + Deck.Deck.Count;
    }

    private void OnMouseExit() {
        DeckInfo.SetActive(false);
    }

}