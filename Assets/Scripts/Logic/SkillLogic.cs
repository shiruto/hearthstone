using System;
using UnityEngine;

public class SkillLogic {
    public SkillCard Skill;
    public int ManaCost;
    public bool isClosed;
    public int HeroPowerDamge;
    public PlayerLogic Owner;

    public SkillLogic(ClassType classType) {
        GameData.HeroPower.TryGetValue(classType, out string HeroPowerName);
        object[] parameters = new object[] { Resources.Load<CardAsset>("ScriptableObject/UnCollectableCard/Skill/" + HeroPowerName) };
        Skill = Activator.CreateInstance(Type.GetType(HeroPowerName.Replace(" ", "")), parameters) as SkillCard;
        ManaCost = Skill.ManaCost;
        Skill.Owner = Owner;
        isClosed = false;
        HeroPowerDamge = 0;
        EventManager.AddListener(TurnEvent.OnTurnStart, OnTurnStartHandler);
    }

    private void OnTurnStartHandler(BaseEventArgs e) {
        if (e.Player == Owner) isClosed = false;
    }

}