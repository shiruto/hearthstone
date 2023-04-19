using UnityEngine;

public class DiscoverOptionController : MonoBehaviour {

    private void OnMouseDown() {
        EventManager.Invoke(EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnDiscover, null, BattleControl.you, GetComponent<BattleCardViewController>().Card));
    }

}
