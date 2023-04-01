using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeckPrevManager : MonoBehaviour {
    public DeckAsset DA;
    public TextMeshProUGUI DeckName;
    public Image DeckImage;
    public GameDataAsset.ClassType DeckClass;
    public int Order;
    public void ReadFromAsset() {
        DeckName.text = DA.name;
        DeckClass = DA.DeckClass;
        Order = DA.Order;
    }
    private void OnMouseDown() {
        Debug.Log("mouse down");
        EventManager.Invoke(EventManager.Allocate<DeckEventArgs>().CreateEventArgs(DeckEvent.OnDeckSelect, gameObject, DA));
    }
}
