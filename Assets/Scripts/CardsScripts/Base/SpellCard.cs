using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellCard : CardBase {
    public GameDataAsset.SpellSchool spellSchool;
    public ICharacter Target;
    public SpellCard(CardAsset CA) : base(CA) {
        spellSchool = CA.SpellSchool;
    }
    public override void Use() {
        Debug.Log("Use a Spell Card");

    }
}
