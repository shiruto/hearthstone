using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class BtnEndTurn : MonoBehaviour {
    public TextMeshProUGUI TxtOpponentsTurn;
    public TextMeshProUGUI TxtEndTurn;

    void Start() {
        GetComponent<Button>().onClick.AddListener(() => {
            SwapText();
            BattleControl.Instance.ActivePlayer.OnTurnEnd();
        });
    }

    public void SwapText() {
        (TxtOpponentsTurn.color, TxtEndTurn.color) = (TxtEndTurn.color, TxtOpponentsTurn.color);
        GetComponent<Button>().interactable = TxtEndTurn.color.a != 0;
    }

}
