public enum CardEvent {
    OnCardUse,
    OnCardGet,
    OnCardDraw,
    OnCardLose,
    OnCardChange,
    OnCardDiscard,
    OnHeroPowerUse,
    OnDiscover
}

public enum VisualEvent {
    DrawLine,
    DeleteLine,
    OnCardReturn
}

public enum TurnEvent {
    OnTurnStart,
    OnTurnEnd,
    OnGameOver
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
    OnManaSpend,
    TemporaryCrystal
}

public enum HeroEvent {
    AfterHeroAttack
}

public enum DeckEvent {
    OnDeckSelect,
    OnDeckComfirm
}

public enum EmptyParaEvent {
    ManaVisualUpdate,
    FieldVisualUpdate,
    DeckVisualUpdate,
    HandVisualUpdate,
    PlayerVisualUpdate
}

public enum AttackEvent {

}
