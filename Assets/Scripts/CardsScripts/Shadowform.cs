using UnityEngine;

public class Shadowform : SpellCard {
    private readonly CardAsset Asset = Resources.Load(GameData.Path + "UnCollectableCard/Skill/Mind Spike") as CardAsset;

    public Shadowform(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        Owner.Skill.ChangeSkill(new MindSpike(Asset));
    }

}