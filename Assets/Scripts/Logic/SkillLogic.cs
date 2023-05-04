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
        HeroPowerDamge = 0;
        ChangeSkill(Activator.CreateInstance(Type.GetType(HeroPowerName.FormatString()), parameters) as SkillCard);
        EventManager.AddListener(TurnEvent.OnTurnStart, OnTurnStartHandler);
    }

    private void OnTurnStartHandler(BaseEventArgs e) {
        if (e.Player == Owner) isClosed = false;
    }

    public void ChangeSkill(SkillCard Skill) {
        this.Skill = Skill;
        ManaCost = Skill.ManaCost;
        Skill.Owner = Owner;
        isClosed = false;
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.SkillVisualUpdate).Invoke();
    }

}