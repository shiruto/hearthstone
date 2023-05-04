public class CrystalChange : Effect {

    public PlayerLogic owner = BattleControl.you;
    public int Crystals;
    public bool isEmpty = false;
    public bool isTemp = false;
    public override string Name => "Crystal Change Effect";

    public CrystalChange(int Crystals, PlayerLogic Owner, bool isEmpty = false, bool isTemp = false) {
        this.Crystals = Crystals;
        owner = Owner;
        this.isEmpty = isEmpty;
        this.isTemp = isTemp;
    }

    public override void ActivateEffect() { // TODO: no temp empty crystal   fix it
        if (isEmpty) EventManager.Allocate<ManaEventArgs>().CreateEventArgs(ManaEvent.OnEmptyCrystalGet, null, owner, Crystals).Invoke();
        else if (isTemp) EventManager.Allocate<ManaEventArgs>().CreateEventArgs(ManaEvent.OnTemporaryCrystalGet, null, owner, Crystals).Invoke();
        else EventManager.Allocate<ManaEventArgs>().CreateEventArgs(ManaEvent.OnPermanentCrystalGet, null, owner, Crystals).Invoke();
    }

}
