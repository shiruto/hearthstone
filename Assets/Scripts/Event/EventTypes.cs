public enum CardEvent {
    OnCardUse,
    OnCardGet,
    OnCardDraw,
    OnCardLose,
    OnCardChange,
    OnCardDiscard
}

public enum VisualEvent {
    DrawLine,
    OnCardReturn
}

public enum TurnEvent {
    OnTurnStart,
    OnTurnEnd
}

public enum MinionEvent {
    BeforeMinonAttack,
    AfterMinionAttack,
    AfterMinionDie,
    AfterMinionSummon,
    AfterMinionStatusChange,
    AfterHealed
}

public enum ManaEvent {
    OnManaSpend
}

public enum HeroEvent {
    AfterHeroAttack
}