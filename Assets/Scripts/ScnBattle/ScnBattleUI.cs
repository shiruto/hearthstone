using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScnBattleUI : MonoBehaviour {
    private Ray ray;
    public static ScnBattleUI Instance;
    public GameObject CardPreview;
    public ICharacter Targeting = null;
    public bool isDragging;
    public GameObject BuffList;
    public GameObject Buff;
    public List<Buff> Buffs;

    private void Awake() {
        Instance = this;
        Application.targetFrameRate = 10;
        EventManager.AddListener(CardEvent.OnCardPreview, OnCardPreviewHandler);
        EventManager.AddListener(CardEvent.AfterCardPreview, AfterCardPreviewHandler);
        CardPreview.SetActive(false);
    }

    private void Update() {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(Camera.main.transform.position, Input.mousePosition, Color.yellow);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, LayerMask.GetMask("UI"))) {
            Transform TargetTrans = hitInfo.collider.transform;
            if (TargetTrans.GetComponent<MinionViewController>()) {
                Targeting = TargetTrans.GetComponent<MinionViewController>().ML;
            }
            else if (TargetTrans.GetComponent<PlayerVisual>()) {
                Targeting = TargetTrans.GetComponent<PlayerVisual>().Player;
            }
            else Targeting = null;
            // Debug.Log(TargetTrans.name + " ICharacter = " + Targeting);
        }
    }

    private void OnCardPreviewHandler(BaseEventArgs e) {
        if (isDragging) return;
        CardEventArgs evt = e as CardEventArgs;
        CardPreview.SetActive(true);
        CardPreview.GetComponent<BattleCardViewController>().Card = evt.Card;
        CardPreview.GetComponent<BattleCardViewController>().ReadFromAsset();
        CardPreview.transform.position = evt.Sender.transform.position - new Vector3(evt.Sender.GetComponent<RectTransform>().sizeDelta.x, 0, 0);
        if (e.Sender.GetComponent<MinionViewController>()) {
            Buffs = new();
            if (e.Sender.GetComponent<MinionViewController>().ML.BuffList.Count != 0) {
                Buffs.AddRange(e.Sender.GetComponent<MinionViewController>().ML.BuffList);
            }
            if (e.Sender.GetComponent<MinionViewController>().ML.Auras.Count != 0) {
                Buffs.AddRange(e.Sender.GetComponent<MinionViewController>().ML.Auras);
            }
            if (Buffs.Count == 0) {
                BuffList.SetActive(false);
                return;
            }
            BuffList.SetActive(true);
            BuffList.GetComponent<RectTransform>().sizeDelta = new Vector2(90, 10);
            if (Buffs.Count > BuffList.transform.childCount) {
                for (int i = 0; i < Buffs.Count; i++) {
                    if (i >= BuffList.transform.childCount) {
                        Instantiate(Buff, BuffList.transform);
                    }
                    BuffList.transform.GetChild(i).gameObject.SetActive(true);
                    BuffList.transform.GetChild(i).transform.localPosition = new Vector3(0, -10 - i * 30, 0);
                    BuffList.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = Buffs[i].BuffName;
                    BuffList.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 30);
                }
            }
            else {
                for (int i = 0; i < BuffList.transform.childCount; i++) {
                    BuffList.transform.GetChild(i).gameObject.SetActive(i < Buffs.Count);
                    if (i < Buffs.Count) {
                        BuffList.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = Buffs[i].BuffName;
                        BuffList.transform.GetChild(i).transform.localPosition = new Vector3(0, -10 - i * 30, 0);
                        BuffList.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 30);
                        //TODO: BuffList.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = Buffs[i].des;
                        //TODO: BuffList.transform.GetChild(i).GetChild(2).GetComponent<Image>() =
                    }
                }
            }
        }
        else {
            BuffList.SetActive(false);
        }
    }

    private void AfterCardPreviewHandler(BaseEventArgs e) {
        CardPreview.SetActive(false);
    }

}
