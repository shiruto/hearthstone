using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SelectedCards: MonoBehaviour {
	public event Action OnCardOverLoad;

	private Transform TxtCardNum;
	private Transform PnlDeckList;
	private Transform SelectedDeckTrans;
	private SortedDictionary<CardAsset, int> CardDictionary = new(new DicCom()); // 自定义的排序方式
	private List<Transform> CardTransList = new();
	public static string DeckName;
	private readonly int MaxCardType = 21;
	private readonly int StartPosY = 800;
	private readonly int CardPrevHeight = 40;
	private int SelectedCardNum = 0;
	private int MaxCardNum = 30;
	public static DeckAsset EditingDeck;

	private void Awake() {
		TxtCardNum = GameObject.Find("TxtCardNum").transform;
		PnlDeckList = GameObject.Find("PnlDeckList").transform;
		SelectedDeckTrans = GameObject.Find("SelectedDeck").transform;

		foreach(Transform Child in transform) {
			Child.localPosition = new(0, -CardTransList.Count * CardPrevHeight + StartPosY - CardPrevHeight / 2, 0);
			CardTransList.Add(Child);
		}
	}
	private void OnEnable() {
		DeckBuilderControl.OnCardClick += OnCardClickHandler;
		DeckBuilderControl.OnCardPrevClick += OnCardPrevClickHandler;
		DeckBuilderControl.OnExitEditing += OnExitEditingHandler;
		Initialize();
	}

	private void Start() {

	}

	private void Initialize() { // 初始化对应卡组 并添加到有序键值对中
		Debug.Log("On initializing deck name = " + EditingDeck.name);
		DeckName = EditingDeck.name;
		SelectedDeckTrans.GetComponent<DeckPrevManager>().DA = EditingDeck;
		SelectedDeckTrans.GetComponent<DeckPrevManager>().ReadFromAsset();
		for(int i = 0; i < EditingDeck.myCardNums.Count; i++) {
			CardDictionary.Add(EditingDeck.myCardAssets[i], EditingDeck.myCardNums[i]);
		}
		Load();
	}

	private void Load() { // 将卡组中的卡牌加载到物体上
		int index = 0;
		foreach(var Card in CardDictionary) {
			CardTransList[index].GetComponent<CardPrevManager>().cardAsset = Card.Key;
			CardTransList[index].GetComponent<CardPrevManager>().CardNum.text = "" + Card.Value;
		}
		if(CardDictionary.Count > MaxCardType) {
			OnCardOverLoad?.Invoke();
		}
		TxtCardNum.GetComponent<TextMeshProUGUI>().text = CardDictionary.Count + "/" + MaxCardNum;
		//Debug.Log("Trans num " + CardTransList.Count + "  Card Dic" + CardDictionary.Count);
	}

	private void OnCardClickHandler(Transform CardTrans) {
		CardAsset newCA = CardTrans.GetComponent<CardManager>().cardAsset;
		if(CardDictionary.Count < MaxCardNum) { // 卡组中的卡牌小于最大卡牌数
			if(!CardDictionary.ContainsKey(newCA)) { // 卡组中不存在这张卡
				CardDictionary.Add(newCA, 1); // 添加这张卡 数量为一
				SelectedCardNum++;
				if(CardTransList.Count > MaxCardType) { // 如果卡牌数量超出显示范围
					OnCardOverLoad?.Invoke();
				}
				Load();
			}
			else if(newCA.rarity.ToString("G") != "Legendary" && CardDictionary[newCA] == 1) { // 如果不是传说卡 且只有一张
				CardDictionary[newCA]++;
				SelectedCardNum++;
				Load();
			}
		}
		else { // 超出上限
			Debug.Log("Too Many Cards");
		}
	}

	private void OnCardPrevClickHandler(Transform CardPrevTrans) {
		CardAsset CA = CardPrevTrans.GetComponent<CardPrevManager>().cardAsset;
		if(CardDictionary[CA] == 1) {
			CardDictionary.Remove(CA);
			SelectedCardNum--;
			Load();
		}
		else if(CardDictionary[CA] >= 2) {
			CardDictionary[CA]--;
			SelectedCardNum--;
			Load();
		}
	}

	public void OnSrlBarValueChangeHandler(float value) {
		transform.localPosition = new(0, -370 + (CardDictionary.Count * CardPrevHeight - 860) * value, 0);
	}

	public void OnInputEnd(string Name) {
		DeckName = Name;
	}

	private void OnExitEditingHandler() {
		SaveDeck();
		PnlDeckList.gameObject.SetActive(true);
		transform.parent.parent.gameObject.SetActive(false);
	}

	private void SaveDeck() {
		EditingDeck.myCardNums = new List<int>();
		EditingDeck.myCardAssets = new List<CardAsset>();
		foreach(var card in CardDictionary) {
			EditingDeck.myCardAssets.Add(card.Key);
			EditingDeck.myCardNums.Add(card.Value);
		}
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	public void OnDeleteHandler() {
		Debug.Log("Delete Button Down");
		File.Delete("Assets/Resources/ScriptableObject/Deck/" + EditingDeck.name + ".asset");
		PnlDeckList.gameObject.SetActive(true);
		transform.parent.parent.gameObject.SetActive(false);
	}

	private void OnDisable() {
		foreach(Transform CardTrans in CardTransList) {
			Destroy(CardTrans.gameObject);
		}
		SelectedCardNum = 0;
		CardDictionary.Clear();
		CardTransList.Clear();
		DeckBuilderControl.OnCardClick -= OnCardClickHandler;
		DeckBuilderControl.OnCardPrevClick -= OnCardPrevClickHandler;
		DeckBuilderControl.OnExitEditing -= OnExitEditingHandler;
	}

	class DicCom: IComparer<CardAsset> {
		public int Compare(CardAsset a, CardAsset b) {
			if(a.ManaCost != b.ManaCost) {
				return a.ManaCost.CompareTo(b.ManaCost);
			}
			else if(a.ManaCost == b.ManaCost) {
				return a.name.CompareTo(b.name);
			}
			else
				return 0;
		}
	}
}
