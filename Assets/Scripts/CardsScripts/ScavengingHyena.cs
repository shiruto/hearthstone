public class ScavengingHyena : MinionCard {

    public ScavengingHyena(CardAsset CA) : base(CA) {
        EventManager.AddListener(MinionEvent.AfterMinionDie, AfterBeastDie);
    }

    public void AfterBeastDie(BaseEventArgs e) {
        GameDataAsset.MinionType MinionType = (e as MinionEventArgs).minion.ca.MinionType;
        if (MinionType == GameDataAsset.MinionType.Beast) {
            Health += 1;
            Attack += 2;
        }
    }

}