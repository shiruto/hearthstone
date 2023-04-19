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

    private void Awake() {
        foreach (Transform child in PnlOpts.transform) {
            CardTrans.Add(child);
        }
        BtnHide.onClick.AddListener(() => {
            foreach (var card in CardTrans) {
                card.gameObject.SetActive(_isHide);
            }
            _isHide = !_isHide;
            (BtnHide.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text, _txt) = (_txt, BtnHide.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        });
        gameObject.SetActive(false);
    }



}
