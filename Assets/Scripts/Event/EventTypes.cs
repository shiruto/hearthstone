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
    CardPreviewDelete,
    WeaponVisualUpdate,
    SkillVisualUpdate
}

public enum AttackEvent {
    BeforeAttack,
    AfterAttack
}

public enum DamageEvent {
    TakeDamage
}
