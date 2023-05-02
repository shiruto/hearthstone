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
    }

    private void OnMouseDrag() {
        if (SL.Skill is not ITarget || SL.isClosed) return;
        EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DrawMinionLine, gameObject, transform.position, Input.mousePosition).Invoke();
    }

    private void OnMouseUp() {
        EventManager.Allocate<VisualEventArgs>().CreateEventArgs(VisualEvent.DeleteLine, gameObject, Vector3.zero, Vector3.zero).Invoke();
        if (!SL.isClosed && SL.Skill is ITarget && (SL.Skill as ITarget).Match(ScnBattleUI.Instance.Targeting)) {
            (SL.Skill as ITarget).Target = ScnBattleUI.Instance.Targeting;
            SL.Skill.Use();
        }
    }

    private void OnMouseEnter() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardPreview, gameObject, null, SL.Skill).Invoke();
    }

    private void OnMouseExit() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject, null, SL.Skill).Invoke();
    }

}