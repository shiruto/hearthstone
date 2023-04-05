using UnityEngine;
using UnityEngine.UI;

public class DeckVisual : MonoBehaviour {
    public DeckLogic Deck;
    public Image DeckStatus;
    private enum Sum { Empty, LastOne, Less, Medium, Alot, Full }
    private Sum CardLeft;

    private void Awake() {
        EventManager.AddListener(EmptyParaEvent.DeckVisualUpdate, UpdateDeckStatus);
    }

    public void UpdateDeckStatus(BaseEventArgs e) {
        switch (Deck.Deck.Count) {
            case >= 60:
                CardLeft = Sum.Full;
                break;
            case > 30:
                CardLeft = Sum.Alot;
                break;
            case > 20:
                CardLeft = Sum.Medium;
                break;
            case > 10:
                CardLeft = Sum.Less;
                break;
            case 1:
                CardLeft = Sum.LastOne;
                break;
            case 0:
                CardLeft = Sum.Empty;
                break;
            default:
                Debug.Log("Wrong Sum of Cards in the Deck");
                break;
        }
        // TODO: asscociate img with Deck.Sum
    }
}