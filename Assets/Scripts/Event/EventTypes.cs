public enum CardEvent {
    OnCardUse,
    OnCardGet,
    OnCardDraw,
    OnCardLose,
    OnCardChange,
    OnCardDiscard,
    OnHeroPowerUse,
    OnDiscover,
    BeforeCardUse,
    OnCardPreview,
    AfterCardPreview,
    OnWeaponEquip,
    OnWeaponDestroy,
    OnSecretReveal
}

public enum VisualEvent {
    DrawCardLine,
    DrawMinionLine,
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
    BeforeMinionSummon,
    AfterMinionSummon,
    AfterMinionStatusChange,
    AfterHealed
}

public enum ManaEvent {
    OnManaSpend,
    OnTemporaryCrystalGet,
    OnPermanentCrystalGet,
    OnEmptyCrystalGet,
    OnManaRecover,
    OnCrystalOverload
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
    PlayerVisualUpdate,
    SecretVisualUpdate,
    CardPreviewDelete
}

public enum AttackEvent {
    BeforeAttack,
    AfterAttack,
    MagicAttack
}
