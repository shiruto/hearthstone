using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardPrevManager : MonoBehaviour {

    public CardAsset cardAsset;
    [Header("Text Component References")]
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI ManaCostText;
    public TextMeshProUGUI CardNum;
    public bool canPreview;

    [Header("Image References")]
    public Image CardGraphicImage;

    public GameObject PfbCard;
    private GameObject CardPreview;

    void Awake() {
        if (cardAsset != null) {
            ReadFromAsset();
        }
    }

    public void ReadFromAsset() {
        NameText.text = cardAsset.name;
        ManaCostText.text = cardAsset.ManaCost.ToString();
    }

    private void OnMouseEnter() {
        if (canPreview) {
            float DiffY = transform.position.y - 610;
            CardPreview = Instantiate(PfbCard, transform.parent.parent.parent);
            CardPreview.GetComponent<CardViewController>().CA = cardAsset;
            if (DiffY > 380) {
                DiffY = 380;
            }
            else if (DiffY < -380) {
                DiffY = -380;
            }
            CardPreview.transform.localPosition = new(-220, DiffY, 0);
            CardPreview.transform.localScale = new(1.5f, 1.5f, 1.5f);
            CardPreview.GetComponent<CardViewController>().ReadFromAsset();
        }
    }

    private void OnMouseExit() {
        if (canPreview) {
            Destroy(CardPreview);
        }
    }
}
