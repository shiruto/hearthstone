using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCard : MinionCard {
    public SkillCard Skill;
    // TODO:
    public HeroCard(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        throw new System.NotImplementedException();
    }

}
