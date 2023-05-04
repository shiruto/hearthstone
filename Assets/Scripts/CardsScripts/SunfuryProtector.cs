using System.Linq;

public class SunfuryProtector : MinionCard, IBattlecryCard {
    private readonly Buff buff;

    public SunfuryProtector(CardAsset CA) : base(CA) {
        buff = new(
            "Sunfury Protector",
            null,
            null,
            new() { CharacterAttribute.Taunt }
        );
    }

    public void BattleCry() {
        int pos = Owner.Field.GetPosition(Minion);
        Effect.GiveAoeBuffEffect(buff, Owner.Field.GetAdjacentMinions(Minion).Cast<IBuffable>().ToList(), (IBuffable b) => true, Minion);
    }

}