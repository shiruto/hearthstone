using UnityEngine.EventSystems;

public class MurlocTidecaller : MinionCard {

    public MurlocTidecaller(CardAsset CA) : base(CA) {

    }

    public void Trigger() {
        EventManager.AddListener(MinionEvent.AfterMinionSummon, EventHandler);
    }

    public void EventHandler(BaseEventArgs e) {
        MinionEventArgs evt = e as MinionEventArgs;
        if (evt.minion.ca.MinionType == GameDataAsset.MinionType.Murloc) {
            Attack++;
        }
    }

}