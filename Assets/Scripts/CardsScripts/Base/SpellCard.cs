using UnityEngine;

public abstract class SpellCard : CardBase {

    public SpellCard(CardAsset CA) : base(CA) {

    }

    public override void Use() {
        Debug.Log("Use a Spell Card");
    }
}
