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
		if(!CardDictionary.ContainsKey(CA)) { // 卡组中没有这张卡
			CardDictionary.Add(CA, 1); // 在字典中创建这张卡牌 并将数量置为一
									   // 卡组没有这张卡 创建一个装卡的物体 并加入卡牌物体列表中
			CardTransList.Add(Instantiate(PfbCardPrev, transform).transform);
			CardTransList[^1].transform.localPosition = new Vector3(0, -CardDictionary.Count * CardPrevHeight + StartPosY, 0);
			if(CardTransList.Count > MaxCardType) { // 如果卡牌种类超过显示上限 加载滚动条
				OnCardOverLoad?.Invoke();
			}
			Debug.Log(CardDictionary[CA]);
			Load(); // 将卡牌重新排序并加载到物体
		}
		else { // 卡组中已经有了
			if(CA.rarity.ToString("G") == "Legendary") { // 如果已经有传说卡 则无法继续添加
				return;
			}
			else if(CardDictionary[CA] >= 2) { // 如果已经有超过两张非传说卡 则无法继续添加
				return;
			}
			else { // 卡牌数量加一
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

	private void Initialize() { // 读取卡组数据 并创建物体 正确挂载卡牌资源


	}

	private void Load() { // 将卡牌加载到对应物体上
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
