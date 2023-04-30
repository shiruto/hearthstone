public class Slience : SpellCard, ITarget {
    public ICharacter Target { get; set; }

    public Slience(CardAsset CA) : base(CA) {

    }

    public bool CanBeTarget(ICharacter Card) {
        return Card is MinionLogic;
    }

    public override void ExtendUse() {
        base.ExtendUse();
        new SlienceEffect(Target).ActivateEffect();
    }

}