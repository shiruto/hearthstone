public class GiveBuff : Effect {
    public Buff BuffToGive;
    public override string Name => "givebuff effect";
    public IBuffable Target { get; set; }
    public IBuffable giver;

    public GiveBuff(Buff buff, IBuffable giver, IBuffable Target = null) {
        BuffToGive = buff;
        this.giver = giver;
        this.Target = Target;
    }

    public override void ActivateEffect() {
        Target?.AddBuff(BuffToGive);
    }

}
