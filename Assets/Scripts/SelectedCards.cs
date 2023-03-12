using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCards: MonoBehaviour {
	public GameObject PfbCardPrev;

	public event Action OnCardOverLoad;

	private SortedDictionary<CardAsset, int> CardDictionary = new(new DicCom());
	private List<Transform> CardTransList = new();
	private readonly int MaxCardType = 21;
	private readonly int StartPosY = 820;
	private readonly int CardPrevHeight = 40;

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
	private void Awake() {
		DeckBuilderControl.OnCardClick += OnCardClickHandler;
		DeckBuilderControl.OnCardPrevClick += OnCardPrevClickHandler;
	}
	
	private void OnCardClickHandler(Transform CardTrans) {
		CardAsset CA = CardTrans.GetComponent<CardManager>().cardAsset;
		if(!CardDictionary.ContainsKey(CA)) { // ������û�����ſ�
			CardDictionary.Add(CA, 1); // ���ֵ��д������ſ��� ����������Ϊһ
									   // ����û�����ſ� ����һ��װ�������� �����뿨�������б���
			CardTransList.Add(Instantiate(PfbCardPrev, transform).transform);
			CardTransList[^1].transform.localPosition = new Vector3(0, -CardDictionary.Count * CardPrevHeight + StartPosY, 0);
			if(CardTransList.Count > MaxCardType) { // ����������೬����ʾ���� ���ع�����
				OnCardOverLoad?.Invoke();
			}
			Debug.Log(CardDictionary[CA]);
			Load(); // �������������򲢼��ص�����
		}
		else { // �������Ѿ�����
			if(CA.rarity.ToString("G") == "Legendary") { // ����Ѿ��д�˵�� ���޷��������
				return;
			}
			else if(CardDictionary[CA] >= 2) { // ����Ѿ��г������ŷǴ�˵�� ���޷��������
				return;
			}
			else { // ����������һ
				CardDictionary[CA]++;
				Load();
			}
		}
	}

	private void OnCardPrevClickHandler(Transform CardPrevTrans) {
		CardAsset CA = CardPrevTrans.GetComponent<CardPrevManager>().cardAsset;
		if(CardDictionary[CA] == 1) {
			CardDictionary.Remove(CA);
			Destroy(CardPrevTrans.gameObject);
		}w
	}

	private void Initialize() { // ��ȡ�������� ���������� ��ȷ���ؿ�����Դ


	}

	private void Load() { // �����Ƽ��ص���Ӧ������
		int i = 0;
		foreach(var card in CardDictionary) {
			CardTransList[i].GetComponent<CardPrevManager>().cardAsset = card.Key;
			CardTransList[i].GetComponent<CardPrevManager>().CardNum.text = "" + card.Value;
			CardTransList[i].GetComponent<CardPrevManager>().ReadCardFromAsset();
			i++;
		}
	}

	private void OnDisable() {
		DeckBuilderControl.OnCardClick -= OnCardClickHandler;
	}
}
