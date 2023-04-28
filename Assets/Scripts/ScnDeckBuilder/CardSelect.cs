using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CardSelect : MonoBehaviour {
    private Transform PnlCards;
    private Transform BtnLastPage;
    private Transform BtnNextPage;
    private List<Transform> CardTrans = new();
    public static List<CardAsset> CardLibrary; // 卡牌资源库
    private List<CardAsset> AvailableCards = new();
    private List<Transform> BtnClassFilter = new();
    private int PageNum = 0;
    private readonly int MaxPageSize = 8;
    private List<int> PageSize = new();
    private string LastClassFilter = "";
    public Dictionary<ClassType, int> ClassIndex = new();
    private Transform TxtPage;
    public static event Func<CardAsset, int> SelectedNum;

    private void Awake() {
        CardLibrary = new List<CardAsset>(Resources.LoadAll<CardAsset>("ScriptableObject/Card"));
        CardLibrary.Sort((CardAsset a, CardAsset b) => {
            if (a.ClassType != b.ClassType) {
                return a.ClassType.CompareTo(b.ClassType);
            }
            if (a.ManaCost != b.ManaCost) {
                return a.ManaCost.CompareTo(b.ManaCost);
            }
            return 0;
        });
        PnlCards = GameObject.Find("PnlCards").transform;
        foreach (Transform Child in PnlCards) {
            CardTrans.Add(Child);
        }
        foreach (Transform Child in GameObject.Find("BgClassSelect").transform) {
            BtnClassFilter.Add(Child);
        }
        BtnLastPage = GameObject.Find("BtnLastPage").transform;
        BtnNextPage = GameObject.Find("BtnNextPage").transform;
        TxtPage = GameObject.Find("TxtPage").transform;
        AvailableCards.AddRange(CardLibrary);
        Initialize();
    }

    private void OnEnable() {
        DeckBuilderControl.OnClassFilter += OnClassFilterHandler;
        DeckBuilderControl.OnCardSearch += FindCardPage;
        DeckList.OnDeckSelect += OnDeckSelectHandler;
        SelectedCards.OnClassFilterOff += OnChangeClass;
        SelectedCards.OnSelectedCardChange += OnSelectedCardChangeHandler;
    }

    private void Initialize() {
        PageSize.Clear();
        PageSize.Add(0);
        foreach (ClassType classType in Enum.GetValues(typeof(ClassType))) {
            ClassIndex[classType] = 0;
        }
        for (int i = 0, j = 0; i < AvailableCards.Count; i++) { // i 为卡牌 j 为当前页的卡牌 初始化所有页的卡牌数
            if ((j != 0) && AvailableCards[i - 1].ClassType != AvailableCards[i].ClassType) { // 某一职业卡牌全部加载完毕
                PageSize.Add(j + PageSize[^1]);
                ClassIndex[AvailableCards[i].ClassType] = PageSize.Count - 1; // 记下每个职业对应的开始页码
                j = 0;
                i--;
            }
            else if (j == MaxPageSize) { // 本页卡牌到达上限
                PageSize.Add(j + PageSize[^1]);
                j = 0;
                i--;
            }
            else {
                j++;
            }
            if (i == AvailableCards.Count - 1) { // Last Page
                PageSize.Add(j + PageSize[^1]);
            }
        }
        Load(0);
    }
    private void Load(int PageIndex) {
        if (PageIndex > PageSize.Count - 1) {
            Debug.Log("Page Index Out of Range");
            return;
        }
        for (int i = 0, j = PageSize[PageIndex]; i < MaxPageSize; i++, j++) {
            if (i < PageSize[PageIndex + 1] - PageSize[PageIndex]) {
                CardTrans[i].gameObject.SetActive(true);
                CardViewController cm = CardTrans[i].GetComponent<CardViewController>();
                cm.CA = AvailableCards[j];
                CardTrans[i].Find("PfbSeletedFrame").gameObject.SetActive(false);
                if (DeckBuilderControl.isEditing) {
                    int tempNum = SelectedNum(cm.CA);
                    CardTrans[i].Find("PfbSeletedFrame").gameObject.SetActive(tempNum > 0);
                    if (tempNum > 0) {
                        CardTrans[i].Find("PfbSeletedFrame").GetComponent<CardFrameManager>().AddCardNum(tempNum);
                    }
                }
                cm.ReadFromAsset();
            }
            else {
                CardTrans[i].gameObject.SetActive(false);
            }
        }
        BtnLastPage.gameObject.SetActive(PageIndex != 0);
        BtnNextPage.gameObject.SetActive(PageIndex != PageSize.Count - 2);
        PageNum = PageIndex;
        TxtPage.GetComponent<TextMeshProUGUI>().text = PageNum + 1 + "/" + (PageSize.Count - 1);
    }

    public void LastPage() {
        PageNum--;
        Load(PageNum);
    }

    public void NextPage() {
        PageNum++;
        Load(PageNum);
    }

    private void OnDisable() {
        DeckBuilderControl.OnClassFilter -= OnClassFilterHandler;
        SelectedCards.OnClassFilterOff -= OnClassFilterHandler;
    }

    private void OnClassFilterHandler(string ClassName) {
        if (LastClassFilter == ClassName) {
            Load(0);
            LastClassFilter = "";
        }
        else {
            Load(ClassIndex[(ClassType)Enum.Parse(typeof(ClassType), ClassName)]);
            LastClassFilter = ClassName;
        }
    }

    private void OnChangeClass(string ClassFilter) {
        if (ClassFilter == "None") {
            foreach (Transform Btn in BtnClassFilter) {
                Btn.gameObject.SetActive(true);
            }
            AvailableCards = new(CardLibrary);
        }
        else {
            AvailableCards = CardLibrary.FindAll(card => card.ClassType.ToString("G") == ClassFilter);
            AvailableCards.AddRange(CardLibrary.FindAll(card => card.ClassType.ToString("G") == "Neutral"));
            foreach (Transform Btn in BtnClassFilter) {
                Btn.gameObject.SetActive(Btn.GetChild(0).GetComponent<TextMeshProUGUI>().text == ClassFilter || Btn.GetChild(0).GetComponent<TextMeshProUGUI>().text == "Neutral");
            }
        }
        Initialize();
    }

    private void OnDeckSelectHandler(string DeckName) {
        DeckAsset DA = AssetDatabase.LoadAssetAtPath<DeckAsset>("Assets/Resources/ScriptableObject/Deck/" + DeckName + ".asset") as DeckAsset;
        Debug.Log("deck click class = " + DA.DeckClass.ToString("G"));
        OnChangeClass(DA.DeckClass.ToString("G"));
    }

    private void FindCardPage(CardAsset CA) {
        int index = AvailableCards.FindIndex((CardAsset a) => a.Equals(CA));
        for (int i = 0; i < PageSize.Count; i++) {
            if (index < PageSize[i]) {
                Load(i - 1);
                return;
            }
        }
        Debug.Log("can't find this card");
    }

    private void OnSelectedCardChangeHandler() {
        Load(PageNum);
    }

}
