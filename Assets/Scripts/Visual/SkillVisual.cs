using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillVisual : MonoBehaviour {
    public SkillCard Skill;
    public Image SkillPic;
    public TextMeshProUGUI ManaCost;

    public void InitSkill(SkillCard sc) {
        Skill = sc;
        ManaCost.text = sc.ManaCost + "";
        // TODO: SkillPic =
    }
}