using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaVisual : MonoBehaviour {
    private readonly Color _colorOn = new(102, 204, 255);
    private readonly Color _colorOff = new(51, 102, 153);

    public ManaLogic Mana;
    public List<Transform> Crystals;
    public GameObject PnlCrystals;
    public TextMeshProUGUI TextMana;

    private void Awake() {
        foreach (Transform child in PnlCrystals.transform) {
            Crystals.Add(child);
        }
        EventManager.AddListener(EmptyParaEvent.ManaVisualUpdate, UpdateCrystals);
    }

    private void UpdateCrystals(BaseEventArgs e) {
        for (int i = 0; i < 10; i++) {
            if (i < Mana.CurCrystals) {
                Crystals[i].gameObject.SetActive(true);
                if (i < Mana.Manas) {
                    Crystals[i].GetComponent<Image>().color = _colorOn;
                }
                else Crystals[i].GetComponent<Image>().color = _colorOff;
            }
            else Crystals[i].gameObject.SetActive(false);
        }
        TextMana.text = Mana.Manas + "/" + Mana.CurCrystals;
    }

}