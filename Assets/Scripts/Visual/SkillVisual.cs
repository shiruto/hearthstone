using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillVisual : MonoBehaviour {
    public SkillLogic SL;
    public Image SkillPic;
    public TextMeshProUGUI ManaCost;
    public GameObject Content;

    private void Awake() {
        EventManager.AddListener(EmptyParaEvent.SkillVisualUpdate, SkillUpdate);
    }

    private void SkillUpdate(BaseEventArgs e) {
        Content.SetActive(!SL.isClosed);
        ReadFromLogic();
    }

    public void ReadFromLogic() {
        ManaCost.text = "" + SL.ManaCost;
        // TODO: pic
    }

    private void OnMouseDown() {
        if (SL.Skill is ITarget || SL.isClosed) return; // TODO: mana check
        SL.Skill.Use();
        SL.isClosed = true;
        SkillUpdate(null);
    }

    private void OnMouseDrag() {
        if (SL.Skill is not ITarget || SL.isClosed) return;
        EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DrawLine, gameObject, transform.position).Invoke();
    }

    private void OnMouseUp() {
        EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DeleteLine, gameObject, Vector3.zero).Invoke();
        if (!SL.isClosed && SL.Skill is ITarget && (SL.Skill as ITarget).Match(ScnBattleUI.Instance.TargetCharacter)) {
            (SL.Skill as ITarget).Target = ScnBattleUI.Instance.TargetCharacter;
            SL.Skill.Use();
            SkillUpdate(null);
        }
    }

    private void OnMouseEnter() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardPreview, gameObject, null, SL.Skill).Invoke();
    }

    private void OnMouseExit() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject, null, SL.Skill).Invoke();
    }

}