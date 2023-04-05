using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnEndTurn : MonoBehaviour {
    public TextMeshProUGUI TxtOpponentsTurn;
    public TextMeshProUGUI TxtEndTurn;

    void Start() {
        GetComponent<Button>().onClick.AddListener(() => {
            (TxtOpponentsTurn.color, TxtEndTurn.color) = (TxtEndTurn.color, TxtOpponentsTurn.color);
            BattleControl.Instance.OnTurnEnd();
        });
    }

}
