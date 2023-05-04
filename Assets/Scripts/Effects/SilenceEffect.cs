public class SilenceEffect : Effect {
    public override string Name => "Slience";
    public ICharacter Target;

    public SilenceEffect(ICharacter Target) {
        this.Target = Target;
    }

    public override void ActivateEffect() {
        Target.Attributes.Clear();
        Target.RemoveAllBuff();
        Target.RemoveAllTriggers();
        if (Target is MinionLogic) {
            (Target as MinionLogic).Deathrattle.Clear();
            (Target as MinionLogic).RemoveAllBuffToGive();
            (Target as MinionLogic).isSilenced = true;
        }
    }

}