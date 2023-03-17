using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DeckList : MonoBehaviour {
    public static event Action<string> OnClassSelected;

    public GameObject PfbDeckPrev;
    private Transform TxtCardNum;
    private Transform SrlBar;
    public Transform PnlCardList;
    private Transform PnlDeckList;
    private Transform PnlClassSelect;
    private Transform BtnNewDeck;
    private List<Transform> DeckTransList = new();
    public static List<DeckAsset> DeckAssetList;

    private readonly int MaxDeckNum = 27; // 最大卡组数
    private readonly int MaxDeckContain = 9; // 没有滑动条时的最大容纳数量
    private readonly int StartPosY = -770; // PnlDeckList 一开始的中心点 Y 值
    private readonly int DeckPrevHeight = 90; // 卡组 Prev 的高度

    private void Awake() {
        DeckBuilderControl.OnDeckPrevClick += OnDeckPrevClickHandler;
        DeckBuilderControl.OnClassSelect += OnClassSelectHandler;

        TxtCardNum = GameObject.Find("TxtCardNum").transform;
        SrlBar = GameObject.Find("DeckSrlBar").transform;
        PnlDeckList = GameObject.Find("MaskDeckList/PnlDeckList").transform;
        PnlClassSelect = GameObject.Find("PnlClassSelect").transform;
        BtnNewDeck = GameObject.Find("BgNewDeck").transform;
        foreach (Transform Child in PnlDeckList) {
            DeckTransList.Add(Child);
        }
    }

    private void OnEnable() {
        LoadDeck();
        if (DeckAssetList.Count > MaxDeckContain) {
            SrlBar.gameObject.SetActive(true);
        }
    }

    private void LoadDeck() {
        AssetDatabase.Refresh();
        DeckAssetList = new List<DeckAsset>(Resources.LoadAll<DeckAsset>("ScriptableObject/Deck"));
        DeckAssetList.Sort((DeckAsset a, DeckAsset b) => a.Order.CompareTo(b.Order));
        for (int i = 0; i < DeckTransList.Count; i++) {
            if (i < DeckAssetList.Count) {
                DeckTransList[i].gameObject.SetActive(true);
                DeckTransList[i].GetComponent<DeckPrevManager>().DA = DeckAssetList[i];
                DeckTransList[i].GetComponent<DeckPrevManager>().ReadFromAsset();
                DeckAssetList[i].Order = i;
            }
            else {
                DeckTransList[i].gameObject.SetActive(false);
            }
        }
        if (DeckAssetList.Count < MaxDeckNum) {
            DeckTransList[^1].gameObject.SetActive(true);
            BtnNewDeck.position = DeckTransList[DeckAssetList.Count].position;
        }
        if (DeckAssetList.Count > MaxDeckContain) {
            SrlBar.gameObject.SetActive(true);
        }
        TxtCardNum.GetComponent<TextMeshProUGUI>().text = DeckAssetList.Count + "/" + MaxDeckNum;
    }

    public void OnBtnNewDeckHandler() { // BtnNewDeck 按钮被按下后触发
        DeckAsset newDA = ScriptableObject.CreateInstance<DeckAsset>();
        string newName = "New Deck";
        for (int i = 0; i < DeckAssetList.Count; i++) {
            if (newName == DeckAssetList[i].name) {
                newName = "New Deck" + (i + 1);
                i = 0;
            }
        }
        newDA.myCardAssets = new List<CardAsset>();
        newDA.myCardNums = new List<int>();
        newDA.Order = DeckAssetList.Count;
        AssetDatabase.CreateAsset(newDA, "Assets/Resources/ScriptableObject/Deck/" + newName + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        SelectedCards.EditingDeck = newDA;
        SelectedCards.DeckName = newName;
        DeckBuilderControl.isSelectingClass = true;
        PnlClassSelect.gameObject.SetActive(true);
    }

    private void OnClassSelectHandler(string CLassName) {
        PnlCardList.gameObject.SetActive(true);
        OnClassSelected?.Invoke(CLassName);
        PnlClassSelect.gameObject.SetActive(false);
        DeckBuilderControl.isEditing = true;
        DeckBuilderControl.isSelectingClass = false;
        gameObject.SetActive(false);
    }

    public void OnSrlBarValueChangeHandler(float value) {
        transform.localPosition = new(0, StartPosY + ((DeckAssetList.Count + 1) * DeckPrevHeight - 890) * value, 0);
    }

    private void OnDeckPrevClickHandler(Transform DeckTrans) {
        SelectedCards.EditingDeck = DeckTrans.GetComponent<DeckPrevManager>().DA;
        OnClassSelected?.Invoke(DeckTrans.GetComponent<DeckPrevManager>().DA.DeckClass.ToString("G"));
        PnlCardList.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
