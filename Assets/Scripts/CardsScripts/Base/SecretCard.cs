using UnityEngine;

public abstract class SecretCard : SpellCard {
    public TriggerStruct trigger;
    public override bool CanBePlayed {
        get => base.CanBePlayed && Owner.Secrets.secrets.Count < 5 && Owner.Secrets.secrets.Contains(this);
    }

    protected SecretCard(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        Debug.Log("Use a Secret Card");
        Owner.Secrets.AddSecret(this);
    }

    public abstract bool SecretImplementation(BaseEventArgs e);

    public virtual void Triggered(BaseEventArgs e) {
        if (BattleControl.Instance.ActivePlayer == Owner) return;
        if (SecretImplementation(e)) {
            Owner.Secrets.RemoveSecret(this);
            EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnSecretReveal, null, Owner, this);
        }
    }

}