using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscoverPnlController : MonoBehaviour {
    public GameObject PnlOpts;
    public Button BtnHide;
    public List<Transform> CardTrans;
    private bool _isHide;
    private string _txt = "Show";
    public int showingCardNum;

    private void Awake() {
        foreach (Transform child in PnlOpts.transform) {
            CardTrans.Add(child);
        }
        BtnHide.onClick.AddListener(() => {
            int i = 0;
            foreach (var card in CardTrans) {
                if (i == showingCardNum) break;
                card.gameObject.SetActive(_isHide);
                i++;
            }
            _isHide = !_isHide;
            (BtnHide.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text, _txt) = (_txt, BtnHide.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        });
        EventManager.AddListener(CardEvent.OnDiscover, (BaseEventArgs e) => gameObject.SetActive(false));
        gameObject.SetActive(false);
    }



}
