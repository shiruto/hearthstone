using System;

public class YouthfulBrewmaster : MinionCard, IBattlecryCard, ITarget {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic && (c as MinionLogic).Owner == Owner;

    public YouthfulBrewmaster(CardAsset CA) : base(CA) {

    }

    public void BattleCry() {
        (Target as MinionLogic).BackToHand();
    }

}