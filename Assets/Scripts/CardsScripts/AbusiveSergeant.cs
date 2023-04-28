using System.Collections.Generic;

public class AbusiveSergeant : MinionCard, ITarget, IBattleCry {
    private readonly Buff _buff;
    public ICharacter Target { get; set; }
    public List<Effect> BattleCryEffects { get; set; }

    public AbusiveSergeant(CardAsset CA) : base(CA) {
        _buff = new(0, 2) {
            BuffName = "abusiveSergeant"
        };
    }

    public bool CanBeTarget(CardBase Card) {
        return Card is MinionCard;
    }

    public void BattleCry() {
        new GiveBuff(_buff, this, ScnBattleUI.Instance.Targeting).ActivateEffect();
    }

}
