using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardSelect: MonoBehaviour {
	private Transform PnlCards;
	private Transform BtnLastPage;
	private Transform BtnNextPage;
	private List<Transform> CardTrans = new();
	public static List<CardAsset> CardLibrary;
	private List<CardAsset> TempCards = new();
	private List<Transform> BtnTransList;

	private int ShownCards = 0;
	private int CurrentCardNum = 0;
	private int PageNum = 0;
	private int MaxPageNum;
	private readonly int MaxPageSize = 8;
	private int[] PageSize = new int[25];
	private string LastClassFilter = "";

	private void Awake() {
		CardLibrary = new List<CardAsset>(Resources.LoadAll<CardAsset>("ScriptableObject"));
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
		DeckBuilderControl.OnBtnClick += OnBtnClickHandler;
		DeckList.OnClassSelected += OnClassSelected;
	}

	private void Initialize(List<CardAsset> TargetCards) {
		ShownCards = 0;
		for(int i = 0; i < TargetCards.Count; i++) {
			if(CurrentCardNum == 8) {
				PageSize[PageNum++] = CurrentCardNum;
				CurrentCardNum = 0;
				i--;
			}
			else if((CurrentCardNum != 0) && TargetCards[i - 1].ClassType != TargetCards[i].ClassType) {
				PageSize[PageNum++] = CurrentCardNum;
				CurrentCardNum = 0;
				i--;
			}
			else {
				CurrentCardNum++;
			}
		}
		PageSize[PageNum] = CurrentCardNum; // ×îºóÒ»Ò³
		CurrentCardNum = 0;
		MaxPageNum = PageNum;
		PageNum = 0;
		Load(TargetCards);
	}
	private void Load(List<CardAsset> TargetCards) {
		for(int i = 0; i < MaxPageSize; i++) {
			if(i < PageSize[PageNum]) {
				CardTrans[i].gameObject.SetActive(true);
				CardManager cm = CardTrans[i].GetComponent<CardManager>();
				cm.cardAsset = TargetCards[ShownCards];
				cm.ReadCardFromAsset();
				ShownCards++;
			}
			else {
				CardTrans[i].gameObject.SetActive(false);
			}
		}
		//Debug.Log("Current Card Number = " + CurrentCardNum + "  ShownCard = " + ShownCards + "  Page number = " + PageNum + "  Last Page = " + PageSize[PageNum - 1 > 0 ? PageNum - 1 : 0]);
		if(PageNum == 0) {
			BtnLastPage.gameObject.SetActive(false);
		}
		else {
			BtnLastPage.gameObject.SetActive(true);
		}
		if(PageNum == MaxPageNum) {
			BtnNextPage.gameObject.SetActive(false);
		}
		else {
			BtnNextPage.gameObject.SetActive(true);
		}
	}

	public void LastPage() {
		ShownCards = ShownCards - PageSize[PageNum - 1] - PageSize[PageNum];
		PageNum--;
		Load(TempCards);
	}

	public void NextPage() {
		PageNum++;
		Load(TempCards);
	}

	private void OnDisable() {
		DeckBuilderControl.OnBtnClick -= OnBtnClickHandler;
	}

	private void OnBtnClickHandler(Transform BtnTrans) {
		TempCards.Clear();
		PageNum = 0;
		ShownCards = 0;
		string ClassName = BtnTrans.GetChild(0).GetComponent<TextMeshProUGUI>().text;
		if(LastClassFilter != ClassName) {
			TempCards = CardLibrary.FindAll(card => card.ClassType.ToString("G") == ClassName);
			LastClassFilter = ClassName;
		}
		else {
			TempCards.AddRange(CardLibrary);
			LastClassFilter = "";
		}
		Initialize(TempCards);
	}

	private void OnClassSelected(string ClassName) {
		TempCards = CardLibrary.FindAll(card => card.ClassType.ToString("G") == ClassName);

		Initialize(TempCards);
	}

}
