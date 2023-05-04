public class DamageEventArgs : BaseEventArgs {
    public ITakeDamage taker;
    public IBuffable source;
    public int Damage;

    public BaseEventArgs CreateEventArgs(DamageEvent eventType, ITakeDamage taker, IBuffable source, ref int damage) {
        base.CreateEventArgs(eventType, null, null);
        this.taker = taker;
        this.source = source;
        Damage = damage;
        return this;
    }
}