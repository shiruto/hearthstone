using UnityEngine;

public class PnlSettings : MonoBehaviour {

    public GameObject Settings;
    private bool isPnlActivated = false;

    public void OnSettingsBtnClick() {
        Settings.SetActive(isPnlActivated);
        isPnlActivated = !isPnlActivated;
    }

    public void OnTurnEndBtnClick() {
        BattleControl.Instance.ActivePlayer.OnTurnEnd();
    }
}
