public class SlienceEffect : Effect {
    public override string Name => "Slience";
    public ICharacter Target;

    public SlienceEffect(ICharacter Target) {
        this.Target = Target;
    }

    public override void ActivateEffect() {
        Target.Attributes.Clear();
        Target.RemoveAllBuff();
        Target.RemoveAllTriggers();
        if (Target is MinionLogic) {
            (Target as MinionLogic).DeathRattleEffects.Clear();
            (Target as MinionLogic).RemoveAllBuffToGive();
        }
    }

}