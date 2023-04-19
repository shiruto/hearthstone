public class VooDooDoctor : MinionCard {
    public VooDooDoctor(CardAsset CA) : base(CA) {
        ifDrawLine = true;
        _battleCryEffects.Add(new DealDamageToTarget(-2, null));
    }
}