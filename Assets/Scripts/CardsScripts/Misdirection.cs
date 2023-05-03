public class Misdirection : SecretCard {

    public Misdirection(CardAsset CA) : base(CA) {
        trigger = new(AttackEvent.BeforeAttack, Triggered);
    }

    public override bool SecretImplementation(BaseEventArgs e) {
        AttackEventArgs evt = e as AttackEventArgs;
        if (evt.target != Owner) return false;
        evt.attacker.AttackTarget = Effect.GetRandomObject(BattleControl.GetAllCharacters(), (ICharacter c) => c == Owner || c == evt.attacker);
        return true;
    }

}