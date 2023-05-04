using System.Collections.Generic;

public class DireWolfAlpha : MinionCard, IAuraMinionCard {
    public List<AuraManager> AuraToGrant { get; }
    private readonly Buff buff;
    List<IBuffable> buffTarget = new(2);

    public DireWolfAlpha(CardAsset CA) : base(CA) {
        buff = new(
            "Dire Wolf Alpha",
            new() { new(Status.Health, Operator.Plus, 1) }
        );
        AuraToGrant = new() { new(buff, (IBuffable c) => c is MinionLogic && !Logic.IsEnemy(Minion, c as MinionLogic) && Minion.Owner.Field.GetAdjacentMinions(Minion).Contains(c as MinionLogic)) };
    }

    private void UpdateAura(BaseEventArgs e) { // TODO: when
        if (buffTarget.Count != 0) {
            foreach (IBuffable b in buffTarget) {
                b.RemoveAura(buff);
            }
        }
        buffTarget = new(Minion.Owner.Field.GetAdjacentMinions(Minion));
        Effect.GiveAoeBuffEffect(buff, buffTarget, (IBuffable b) => true, Minion);
    }

}