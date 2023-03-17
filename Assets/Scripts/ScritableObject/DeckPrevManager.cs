using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckPrevManager : MonoBehaviour {
    public DeckAsset DA;
    public TextMeshProUGUI DeckName;
    public Image DeckImage;
    public GameDataAsset.ClassType DeckClass;
    public int Order;

    void Awake() {
        if (DA != null) {
            ReadFromAsset();
        }
    }

    public void ReadFromAsset() {
        DeckName.text = DA.name;
        DeckClass = DA.DeckClass;
        Order = DA.Order;
    }
}
