public class ElvenArcher : MinionCard {
    public ElvenArcher(CardAsset CA) : base(CA) {
        _battleCryEffects.Add(new DealDamageToTarget(1, null));
    }
}