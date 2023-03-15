using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SelectedCards: MonoBehaviour {
	public GameObject PfbCardPrev;

	public event Action<int> OnCardOverLoad;
	public static event Action<string> OnDeckDelete;

	private SortedDictionary<CardAsset, int> CardDictionary = new(new DicCom()); // ���� ��ֵ�� ��������:����
	private List<Transform> CardTransList = new();
	private Transform TxtCardNum;
	private Transform PnlDeckList;
	private Transform TxtSelectedDeckName;
	public static string DeckName;
	private readonly int MaxCardType = 21;
	private readonly int StartPosY = 820;
	private readonly int CardPrevHeight = 40;
	private int SelectedCardNum = 0;
	private int MaxCardNum = 30;
	private int EndPos;

	private void Awake() {
		TxtCardNum = GameObject.Find("TxtCardNum").transform;
		PnlDeckList = GameObject.Find("PnlDeckList").transform;
	}
	private void OnEnable() {
		DeckBuilderControl.OnCardClick += OnCardClickHandler;
		DeckBuilderControl.OnCardPrevClick += OnCardPrevClickHandler;
		DeckBuilderControl.OnExitEditing += OnExitEditingHandler;
		Initialize(DeckName);
	}

	private void Start() {
		
	}
	private void Initialize(string newDeckName) { // ��ȡ�������� ���������� ��ȷ���ؿ�����Դ
		Debug.Log("On initializing deck name = " + newDeckName);
		DeckAsset SO_Deck = Resources.Load<DeckAsset>("ScriptableObject/Deck/" + newDeckName);
		GameObject.Find("TxtSelectedDeckName").GetComponent<TMP_InputField>().text = newDeckName; // �������Ƹ�ֵ

		if(SO_Deck == null) {
			Debug.Log("Deck Don't Exist");
			return;
		}

		for(int i = 0; i < SO_Deck.myCardNums.Count; i++) {
			CardDictionary.Add(SO_Deck.myCardAssets[i], SO_Deck.myCardNums[i]);
			Transform newCardTrans = Instantiate(PfbCardPrev, transform).transform;
			CardTransList.Add(newCardTrans);
			newCardTrans.localPosition = new Vector3(0, -CardTransList.Count * CardPrevHeight + StartPosY, 0);
			CardTransList[i].GetComponent<CardPrevManager>().cardAsset = SO_Deck.myCardAssets[i];
			CardTransList[i].GetComponent<CardPrevManager>().CardNum.text = "" + SO_Deck.myCardNums[i];
			SelectedCardNum += SO_Deck.myCardNums[i];
			CardTransList[i].GetComponent<CardPrevManager>().ReadCardFromAsset();
		}

		if(CardTransList.Count > MaxCardType) { // ����������೬����ʾ���� ���ع�����
			OnCardOverLoad?.Invoke(SelectedCardNum);
		}
		TxtCardNum.GetComponent<TextMeshProUGUI>().text = SelectedCardNum + "/" + MaxCardNum;
	}

	private void Load() { // �����Ƽ��ص���Ӧ������
		int index = 0;
		foreach(var card in CardDictionary) {
			CardTransList[index].GetComponent<CardPrevManager>().cardAsset = card.Key;
			CardTransList[index].GetComponent<CardPrevManager>().CardNum.text = "" + card.Value;
			CardTransList[index].GetComponent<CardPrevManager>().ReadCardFromAsset();
			index++;
		}
		TxtCardNum.GetComponent<TextMeshProUGUI>().text = SelectedCardNum + "/" + MaxCardNum;
		//Debug.Log("Trans num " + CardTransList.Count + "  Card Dic" + CardDictionary.Count);
	}

	private void OnCardClickHandler(Transform CardTrans) {
		CardAsset CA = CardTrans.GetComponent<CardManager>().cardAsset;
		if(SelectedCardNum < MaxCardNum) {
			if(!CardDictionary.ContainsKey(CA)) { // ������û�����ſ�
				CardDictionary.Add(CA, 1); // ���ֵ��д������ſ��� ����������Ϊһ
				Transform newCardTrans = Instantiate(PfbCardPrev, transform).transform;
				CardTransList.Add(newCardTrans);
				newCardTrans.localPosition = new Vector3(0, -CardDictionary.Count * CardPrevHeight + StartPosY, 0);
				SelectedCardNum++;
				if(CardTransList.Count > MaxCardType) { // ����������೬����ʾ���� ���ع�����
					OnCardOverLoad?.Invoke(SelectedCardNum);
				}
				Load(); // �������������򲢼��ص�����
				TxtCardNum.GetComponent<TextMeshProUGUI>().text = SelectedCardNum + "/" + MaxCardNum;
				EndPos = CardDictionary.Count * CardPrevHeight;
			}
			else { // �������Ѿ�����
				if(CA.rarity.ToString("G") == "Legendary") { // ����Ѿ���ͬ����˵�� ���޷��������
					return;
				}
				else if(CardDictionary[CA] >= 2) { // ����Ѿ��г������ŷǴ�˵�� ���޷��������
					return;
				}
				else { // ����������һ
					CardDictionary[CA]++;
					SelectedCardNum++;
					Load();
					TxtCardNum.GetComponent<TextMeshProUGUI>().text = SelectedCardNum + "/" + MaxCardNum;
					EndPos = CardDictionary.Count * CardPrevHeight;
				}
			}
		}
		else {
			Debug.Log("Too Many Cards");
		}
	}

	private void OnCardPrevClickHandler(Transform CardPrevTrans) {
		CardAsset CA = CardPrevTrans.GetComponent<CardPrevManager>().cardAsset;
		if(CardDictionary[CA] == 1) {
			CardDictionary.Remove(CA);
			Destroy(CardTransList[^1].gameObject);
			CardTransList.RemoveAt(CardTransList.Count - 1);
			SelectedCardNum--;
			EndPos = CardDictionary.Count * CardPrevHeight;
			Load();
		}
		else if(CardDictionary[CA] >= 2) {
			CardDictionary[CA]--;
			SelectedCardNum--;
			Load();
		}
	}

	public void OnSrlBarValueChangeHandler(float value) {
		transform.localPosition = new Vector3(0, -370 + (EndPos - 860) * value, 0);
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
		if(DeckName == "") {
			DeckName = "New Deck";
		}
		Debug.Log("On saving Deck Name = " + DeckName);
		DeckAsset SODeck = Resources.Load<DeckAsset>("ScriptableObject/Deck/" + DeckName);
		if(!SODeck) {
			Debug.Log("can't find the asset");
			SODeck = ScriptableObject.CreateInstance<DeckAsset>();
			AssetDatabase.CreateAsset(SODeck, "Assets/Resources/ScriptableObject/Deck/" + DeckName + ".asset");
			SODeck.Order = DeckList.DeckNum;
		}
		SODeck.myCardNums = new List<int>();
		SODeck.myCardAssets = new List<CardAsset>();
		foreach(var card in CardDictionary) {
			SODeck.myCardAssets.Add(card.Key);
			SODeck.myCardNums.Add(card.Value);
		}
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	public void OnDeleteHandler() {
		Debug.Log("Delete Button Down");
		File.Delete("Assets/Resources/ScriptableObject/Deck/" + DeckName + ".asset");
		OnDeckDelete?.Invoke(DeckName);
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
			else return 0;
		}
	}
}
