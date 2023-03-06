using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TargetingOptions { // 卡牌目标选项 
	NoTarget,
	AllMinions,
	EnemyMinions,
	YourMinions,
	AllCharacters,
	EnemyCharacters,
	YourCharacters
}

public enum Harm { None, Unharmed, Harmed }
public enum MinionType { None, Murloc, Demon, Mech, Elemental, Beast, Totem, Pirate, Dragon, Quilboar, Naga, Undead, All }
public enum SpellSchool { None, Arcane, Fire, Frost, Nature, Holy, Shadow, Fel }
public enum Rarity { Free, Normal, Rare, Epic, Legendary }
public enum ClassType { None, DemonHunter, Druid, Hunter, Mage, Paladin, Priest, Rouge, Shaman, Warloc, Warrior }

public class CardAsset: ScriptableObject {
	//[Header("General info")]
	//public CharacterAsset characterAsset;  // 卡牌所属角色
	[TextArea(2, 3)]
	public string Description; // 卡牌描述
	public Sprite CardImage; // 卡牌图像
	public int ManaCost; // 卡牌消耗
	public int OrgManaCost; // 原卡牌消耗
	public Rarity rarity; // 稀有度
	public ClassType ClassType; // 卡牌所属职业
	public bool isTriggered; // 是否被触发

	[Header("Creature Info")] // 作为生物卡牌的信息
	public int Attack; // 攻击力
	public int CurAttack; // 现攻击
	public int MaxHealth; // 最大生命值
	public int CurHealth; // 现生命
	public int AttacksChances = 1; // 一回合内攻击次数
	public int SpellDamage = 0; // 法术伤害
	public int specialCreatureAmount; // 技能数值
	public bool isWeapon; // 是否是武器
	public bool isTaunt; // 是否嘲讽
	public bool isCharge; // 是否冲锋，即刚入场的回合是否可以立即攻击
	public bool isRush; // 是否突袭
	public bool isDivineShield; // 是否圣盾
	public bool isStealth;  // 是否潜行
	public bool isImmune; // 是否免疫
	public bool isFrozen; // 是否被冻结
	public bool isPoison; // 是否剧毒
	public string CreatureScriptName; // 生物脚本名
	public MinionType MinionType; // 随从类型

	[Header("SpellInfo")]
	public string SpellScriptName; // 技能脚本名
	public int specialSpellAmount; // 技能数值
	public bool isSecret; // 是否为奥秘
	public TargetingOptions TargetsType; // 技能对象选择
	public MinionType TargetMinionType; // 随从类型选择
	public Harm TargetsHarm; // 随从是否受伤选择
	public SpellSchool SpellSchool; // 法术派系

}