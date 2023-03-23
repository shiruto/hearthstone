using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataAsset : ScriptableObject {
    public enum ClassType { DemonHunter, Druid, Hunter, Mage, Paladin, Priest, Rouge, Shaman, Warloc, Warrior, Neutral }
    public enum SpellSchool { Arcane, Frost, Fire, Nature, Fel, Shadow, Holy, None }
    public enum MinionType { Beast, Demon, Undead, Totem, Murloc, Mech, Elemental, Dragon, Pirate, Quilboar, Naga, None }
    public enum TargetingOptions { NoTarget, AllMinions, EnemyMinions, YourMinions, AllCharacters, EnemyCharacters, YourCharacters }
    public enum Harm { None, Unharmed, Harmed }
    public enum Rarity { Free, Normal, Rare, Epic, Legendary }
}
