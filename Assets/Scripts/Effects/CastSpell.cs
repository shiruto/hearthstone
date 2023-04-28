public class CastSpell : Effect {
    public override string Name => "CastSpell";
    public SpellCard SpellToCast;

    public CastSpell(SpellCard sc) {
        SpellToCast = sc;
    }

    public CastSpell() {
        SpellToCast = null;
    }

    public override void ActivateEffect() {
        if (SpellToCast == null) {
            // TODO: cast a random spell
        }
        else SpellToCast.ExtendUse();
    }

}