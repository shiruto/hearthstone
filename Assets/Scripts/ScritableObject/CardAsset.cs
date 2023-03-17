using UnityEngine;

public enum TargetingOptions { // ����Ŀ��ѡ�� 
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
public enum ClassType { DemonHunter, Druid, Hunter, Mage, Paladin, Priest, Rouge, Shaman, Warloc, Warrior, Neutral }

public class CardAsset : ScriptableObject {
    //[Header("General info")]
    //public CharacterAsset characterAsset;  // ����������ɫ
    [TextArea(2, 3)]
    public string Description; // ��������
    public Sprite CardImage; // ����ͼ��
    public int ManaCost; // ��������
    public int OrgManaCost; // ԭ��������
    public Rarity rarity; // ϡ�ж�
    public ClassType ClassType; // ��������ְҵ
    public bool isTriggered; // �Ƿ񱻴���
    public CardManager Script;

    [Header("Creature Info")] // ��Ϊ���￨�Ƶ���Ϣ
    public int Attack; // ������
    public int CurAttack; // �ֹ���
    public int MaxHealth; // �������ֵ
    public int CurHealth; // ������
    public int AttacksChances = 1; // һ�غ��ڹ�������
    public int SpellDamage = 0; // �����˺�
    public int specialCreatureAmount; // ������ֵ
    public bool isWeapon; // �Ƿ�������
    public bool isTaunt; // �Ƿ񳰷�
    public bool isCharge; // �Ƿ��棬�����볡�Ļغ��Ƿ������������
    public bool isRush; // �Ƿ�ͻϮ
    public bool isDivineShield; // �Ƿ�ʥ��
    public bool isStealth;  // �Ƿ�Ǳ��
    public bool isImmune; // �Ƿ�����
    public bool isFrozen; // �Ƿ񱻶���
    public bool isPoison; // �Ƿ�綾
    public string CreatureScriptName; // ����ű���
    public MinionType MinionType; // �������

    [Header("SpellInfo")]
    public string SpellScriptName; // ���ܽű���
    public int specialSpellAmount; // ������ֵ
    public bool isSecret; // �Ƿ�Ϊ����
    public TargetingOptions TargetsType; // ���ܶ���ѡ��
    public MinionType TargetMinionType; // �������ѡ��
    public Harm TargetsHarm; // ����Ƿ�����ѡ��
    public SpellSchool SpellSchool; // ������ϵ

}