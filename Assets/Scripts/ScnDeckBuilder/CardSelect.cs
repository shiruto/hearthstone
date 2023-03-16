using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardSelect: MonoBehaviour {
	private Transform PnlCards;
	private Transform BtnLastPage;
	private Transform BtnNextPage;
	private List<Transform> CardTrans = new();
	public static List<CardAsset> CardLibrary; // 卡牌资源库
	private List<CardAsset> TempCards = new();

	private int PageNum = 0;
	private readonly int MaxPageSize = 8;
	private List<int> PageSize = new();
	private string LastClassFilter = "";
	private Dictionary<string, int> ClassIndex = new();

	private void Awake() {
		ClassIndexInitialize();
		CardLibrary = new List<CardAsset>(Resources.LoadAll<CardAsset>("ScriptableObject/Card"));
		CardLibrary.Sort((CardAsset a, CardAsset b) => {
			if(a.ClassType != b.ClassType) {
				return a.ClassType.CompareTo(b.ClassType);
			}
			if(a.ManaCost != b.ManaCost) {
				return a.ManaCost.CompareTo(b.ManaCost);
			}
			return 0;
		});
		PnlCards = GameObject.Find("PnlCards").transform;
		foreach(Transform Child in PnlCards) {
			CardTrans.Add(Child);
		}
		BtnLastPage = GameObject.Find("BtnLastPage").transform;
		BtnNextPage = GameObject.Find("BtnNextPage").transform;
		TempCards.AddRange(CardLibrary);
		Initialize(TempCards);
	}

	private void OnEnable() {
		DeckBuilderControl.OnClassFilter += OnClassFilterHandler;
		DeckList.OnClassSelected += OnClassSelectedHandler;
	}

	private void Initialize(List<CardAsset> TargetCards) {
		PageSize.Clear();
		for(int i = 0, j = 0; i < TargetCards.Count; i++) { // i 为卡牌 j 为当前页的卡牌
			if(j == 8) { // 本页卡牌到达上限
				PageSize.Add(j + ((PageSize.Count == 0) ? 0 : PageSize[^1]));
				j = 0;
				i--;
			}
			else if((j != 0) && TargetCards[i - 1].ClassType != TargetCards[i].ClassType) { // 某一职业卡牌全部加载完毕
				PageSize.Add(j + ((PageSize.Count == 0) ? 0 : PageSize[^1]));
				ClassIndex[TargetCards[i].ClassType.ToString("G")] = PageNum; // 记下每个职业对应的开始页码
				j = 0;
				i--;
			}
			else {
				j++;
			}
			if(i == TargetCards.Count - 1) { // Last Page
				PageSize.Add(j + ((PageSize.Count == 0) ? 0 : PageSize[^1]));
			}
		}
		TempCards.Clear();
		TempCards = new(TargetCards);
		Load(0);
	}

	private void Load(int PageIndex) {
		if(PageIndex > PageSize.Count) {
			Debug.Log("Page Index Out of Range");
			return;
		}
		for(int i = 0, j = (PageNum == 0) ? 0 : PageSize[PageNum] - PageSize[PageNum - 1]; i < MaxPageSize; i++, j++) {
			if(i < PageSize[PageNum]) {
				CardTrans[i].gameObject.SetActive(true);
				CardManager cm = CardTrans[i].GetComponent<CardManager>();
				cm.cardAsset = TempCards[j];
				cm.ReadCardFromAsset();
			}
			else {
				CardTrans[i].gameObject.SetActive(false);
			}
		}
		//Debug.Log("Current Card Number = " + CurrentCardNum + "  ShownCard = " + ShownCards + "  Page number = " + PageNum + "  Last Page = " + PageSize[PageNum - 1 > 0 ? PageNum - 1 : 0]);
		BtnLastPage.gameObject.SetActive(PageNum != 0);
		BtnNextPage.gameObject.SetActive(PageNum != PageSize.Count);
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
	}

	private void OnClassFilterHandler(string ClassName) {
		if(LastClassFilter == ClassName) {
			Load(0);
			LastClassFilter = "";
		}
		else {
			Load(ClassIndex[ClassName]);
			LastClassFilter = ClassName;
		}
	}

	private void OnClassSelectedHandler(string ClassName) {
		TempCards = CardLibrary.FindAll(card => card.ClassType.ToString("G") == ClassName);
		TempCards.AddRange(CardLibrary.FindAll(card => card.ClassType.ToString("G") == "Neutral"));
		Initialize(TempCards);
		Load(0);
	}

	private void ClassIndexInitialize() {
		ClassIndex.Add("DemonHunter", 0);
		ClassIndex.Add("Druid", 0);
		ClassIndex.Add("Hunter", 0);
		ClassIndex.Add("Mage", 0);
		ClassIndex.Add("Paladin", 0);
		ClassIndex.Add("Priest", 0);
		ClassIndex.Add("Rouge", 0);
		ClassIndex.Add("Shaman", 0);
		ClassIndex.Add("Warloc", 0);
		ClassIndex.Add("Warrior", 0);
		ClassIndex.Add("Neutral", 0);
	}

}
