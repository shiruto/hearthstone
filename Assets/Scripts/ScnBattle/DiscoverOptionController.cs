using UnityEngine;

public class DiscoverOptionController : MonoBehaviour {

    private void OnMouseDown() {
        BattleControl.you.Hand.GetCard(-1, GetComponent<BattleCardViewController>().Card);
    }

}
