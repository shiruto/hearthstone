using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalChange : Effect {
    public PlayerLogic owner = BattleControl.you;
    public int Crystals;
    public bool isEmpty = true;
    public override string Name => "Crystal Change Effect";
    public CrystalChange(int Crystals) {
        this.Crystals = Crystals;
    }
    public CrystalChange(PlayerLogic owner, int Crystals, bool isEmpty) {
        this.owner = owner;
        this.Crystals = Crystals;
        this.isEmpty = isEmpty;
    }
    public override void ActivateEffect() {
        if (isEmpty) {
            owner.Mana.GainEmptyCrystal(Crystals);
        }
        else owner.Mana.CurCrystals += Crystals;
    }
}
