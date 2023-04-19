public abstract class SecretCard : SpellCard {
    protected SecretCard(CardAsset CA) : base(CA) {

    }

    public override void Use() {
        owner.Secrets.Add(this);
    }
}