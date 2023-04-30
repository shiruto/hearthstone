using UnityEngine;

public abstract class SecretCard : SpellCard {

    protected SecretCard(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        Debug.Log("Use a Secret Card");
        Owner.Secrets.AddSecret(this);
    }

    public abstract void AddTrigger();

    public abstract void DelTrigger();

    public abstract void SecretImplement(BaseEventArgs e, out bool isTriggered);

    public virtual void Triggered(BaseEventArgs e) {
        if (Owner != BattleControl.Instance.ActivePlayer) {
            SecretImplement(e, out bool isTriggered);
            if (isTriggered) {
                EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnSecretReveal, null, Owner, this).Invoke();
                DelTrigger();
                Owner.Secrets.RemoveSecret(this);
            }
        }
    }

}