using System.Collections.Generic;

public class YoggSaronHopesEnd : MinionCard, IBattleCry {
    public List<Effect> BattleCryEffects { get; set; }
    public int SpellCast;

    public YoggSaronHopesEnd(CardAsset CA) : base(CA) {
        EventManager.AddListener(CardEvent.OnCardUse, Triggered);
    }

    private void Triggered(BaseEventArgs e) {
        if (e.Player != Owner) {
            return;
        }
        CardEventArgs evt = e as CardEventArgs;
        if (evt.Card.CA.cardType == CardType.Spell) {
            SpellCast++;
        }
    }

    public void BattleCry() {
        for (int i = SpellCast; i > 0; i--) {
            new CastSpell().ActivateEffect();
        }
    }

}