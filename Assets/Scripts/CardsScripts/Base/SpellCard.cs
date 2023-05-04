using System.Collections.Generic;
using UnityEngine;

public abstract class SpellCard : CardBase {

    public SpellCard(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        Debug.Log("Cast a Spell");
    }

}
