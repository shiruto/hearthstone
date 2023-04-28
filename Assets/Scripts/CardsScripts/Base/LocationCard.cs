public class LocationCard : CardBase {
    public int Health;

    public LocationCard(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        Health -= 1;
    }

}