using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckConfirm : MonoBehaviour {
    public TextMeshProUGUI DeckName;
    private void Awake() {
        GetComponent<Button>().onClick.AddListener(() => Global.DeckName = DeckName.text);
    }
}
