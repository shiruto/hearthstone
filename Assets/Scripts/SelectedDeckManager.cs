using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedDeckManager : MonoBehaviour {
    public Image DeckPic;
    public TextMeshProUGUI DeckName;
    public Image SkillPic;
    public TextMeshProUGUI SkillCost;
    private void Awake() {
        EventManager.AddListener(DeckEvent.OnDeckSelect, ReadFromAsset);
    }

    private void ReadFromAsset(BaseEventArgs _eventData) {
        DeckEventArgs _event = _eventData as DeckEventArgs;
        // TODO: DeckPic = _event.Deck.;
        DeckName.text = _event.Deck.name;
        SkillCost.text = 2 + "";
        //TODO SkillPic = _event.Deck.ClassType
    }
}
