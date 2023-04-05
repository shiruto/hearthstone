using System.Collections.Generic;
using UnityEngine;

public class MinionCardAsset : CardAsset {

    public int Attack;
    public int MaxHealth;
    public int SpellDamage = 0;
    public bool isWindFury;
    public bool isTaunt;
    public bool isCharge;
    public bool isRush;
    public bool isDivineShield;
    public bool isStealth;
    public bool isImmune;
    public bool isFrozen;
    public bool isPoisonous;
    public string CreatureScriptName;
    public GameDataAsset.MinionType MinionType;

    public List<GameDataAsset.EffectType> BattleCryEffects;
    public List<int> BattleCryEffectsNum;
    public List<GameDataAsset.EffectType> DeathRattleEffects;
    public List<int> DeathRattleEffectsNum;

}