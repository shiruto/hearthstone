public class GiveBuff : Effect {
    public Buff BuffToGive;
    public override string Name => "givebuff effect";
    public IBuffable Target { get; set; }
    public CardBase giver;

    public GiveBuff(Buff buff, CardBase giver, IBuffable Target = null) {
        BuffToGive = buff;
        this.giver = giver;
        this.Target = Target;
    }

    public override void ActivateEffect() {
        Target.AddBuff(BuffToGive);
    }

}
