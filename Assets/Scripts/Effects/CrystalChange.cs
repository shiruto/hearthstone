public class CrystalChange : Effect {

    public PlayerLogic owner = BattleControl.you;
    public int Crystals;
    public bool isEmpty = false;
    public bool isTemp = false;
    public override string Name => "Crystal Change Effect";

    public CrystalChange(int Crystals) {
        this.Crystals = Crystals;
    }

    public CrystalChange(bool isTemp, int Crystals) {
        this.isTemp = isTemp;
        this.Crystals = Crystals;
    }

    public CrystalChange(PlayerLogic owner, int Crystals, bool isEmpty) {
        this.owner = owner;
        this.Crystals = Crystals;
        this.isEmpty = isEmpty;
    }

    public override void ActivateEffect() { // TODO: no temp empty crystal
        if (isEmpty) EventManager.Allocate<ManaEventArgs>().CreateEventArgs(ManaEvent.OnEmptyCrystalGet, null, owner, Crystals).Invoke();
        else if (isTemp) EventManager.Allocate<ManaEventArgs>().CreateEventArgs(ManaEvent.OnTemporaryCrystalGet, null, owner, Crystals).Invoke();
        else EventManager.Allocate<ManaEventArgs>().CreateEventArgs(ManaEvent.OnPermanentCrystalGet, null, owner, Crystals).Invoke();
    }
}
