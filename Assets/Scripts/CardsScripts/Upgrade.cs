using UnityEngine;

public class Upgrade : SpellCard {
    private readonly CardAsset weapon;
    private readonly Buff buff;

    public Upgrade(CardAsset CA) : base(CA) {
        weapon = Resources.Load(GameData.Path + "UnCollectableCard/HeavyAxe") as CardAsset;
        buff = new(
            "Upgrade!",
            new() { new(Status.Attack, Operator.Plus, 1), new(Status.Health, Operator.Plus, 1) }
        );
    }

    public override void ExtendUse() {
        base.ExtendUse();
        if (Owner.Weapon.Card == null) {
            Owner.Weapon.EquipWeapon(new DefaultWeapon(weapon));
        }
        else {
            (Owner.Weapon as IBuffable).AddBuff(buff);
        }
    }

}