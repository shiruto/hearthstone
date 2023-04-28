using UnityEngine;

public class DiscoverOptionController : MonoBehaviour {

    private void OnMouseDown() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnDiscover, null, null, GetComponent<BattleCardViewController>().Card).Invoke();
    }

}
