using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DeckList: MonoBehaviour {
	public static event Action<string> OnClassSelected;
	public static int DeckNum = 0; // 已创建物体的卡组数

	public GameObject PfbDeckPrev;
	private Transform SrlBar;
	private Transform PnlCardList;
	private Transform PnlDeckList;
	private Transform PnlClassSelect;
	private Transform BtnNewDeck;
	private List<Transform> DeckListTrans = new();
	public static List<DeckAsset> DeckAssetList;
	
	private readonly int MaxDeckNum = 27;
	private readonly int MaxDeckContain = 9; // 无滚动条最大卡组数量
	private readonly int StartPosY = 1215;
	private readonly int DeckPrevHeight = 90;
	private int EndPos;

	private void Awake() {
		DeckBuilderControl.OnDeckPrevClick += OnDeckPrevClickHandler;
		DeckBuilderControl.OnClassSelect += OnClassSelectHandler;
		SelectedCards.OnDeckDelete += OnDeckDeleteHandler;

		SrlBar = GameObject.Find("DeckSrlBar").transform;
		PnlCardList = GameObject.Find("PnlCardList").transform;
		PnlDeckList = GameObject.Find("MaskDeckList/PnlDeckList").transform;
		PnlClassSelect = GameObject.Find("PnlClassSelect").transform;
		BtnNewDeck = GameObject.Find("BgNewDeck").transform;
	}

	private void OnEnable() {
		LoadDeck();
		if(DeckNum > MaxDeckContain) {
			SrlBar.gameObject.SetActive(true);
		}
	}

	private void LoadDeck() {
		AssetDatabase.Refresh();
		DeckAssetList = new List<DeckAsset>(Resources.LoadAll<DeckAsset>("ScriptableObject/Deck"));
		DeckAssetList.Sort((DeckAsset a, DeckAsset b) => a.Order.CompareTo(b.Order));
		Debug.Log("文件中卡组总数: " + DeckAssetList.Count + "已加载的卡组数: " + DeckNum);
		if(DeckListTrans.Count > 0 ) {
			DeckListTrans.Remove(BtnNewDeck);
		}
		for(int i = 0; i < DeckAssetList.Count; i++) {
			if(i >= DeckNum) { // 如果正在更新的卡牌是新增卡牌 则增加新物体并更新名字 如果不是 则只更新名字
				Debug.Log("i = " + i + " 新增物体");
				Transform newDeckTrans;
				newDeckTrans = Instantiate(PfbDeckPrev, PnlDeckList).transform;
				newDeckTrans.name = "Deck" + i;
				newDeckTrans.localPosition = new Vector3(0, StartPosY - DeckPrevHeight / 2 - DeckPrevHeight * i, 0);
				newDeckTrans.Find("TxtDeckName").GetComponent<TextMeshProUGUI>().text = DeckAssetList[i].name;
				 // 需处理NEWDeck按钮的位置
				DeckListTrans.Add(newDeckTrans);
			}
			Debug.Log("物体数: " + DeckListTrans.Count);
			Debug.Log($"number {i + 1} Deck, Deck Trans Name = {DeckListTrans[i].Find("TxtDeckName").GetComponent<TextMeshProUGUI>().text}");
			DeckListTrans[i].Find("TxtDeckName").GetComponent<TextMeshProUGUI>().text = DeckAssetList[i].name;
		}
		DeckNum = DeckAssetList.Count;
		if(DeckNum < MaxDeckNum) {
			BtnNewDeck.localPosition = new Vector3(0, StartPosY - DeckPrevHeight / 2 - DeckPrevHeight * DeckNum, 0);
			DeckListTrans.Add(BtnNewDeck);
		}
		if(DeckNum > MaxDeckContain) {
			SrlBar.gameObject.SetActive(true);
		}
		EndPos = DeckNum * DeckPrevHeight;
	}

	public void OnBtnNewDeckHandler() { // 响应"新建套牌"按钮点击事件
		SelectedCards.DeckName = "";
		DeckBuilderControl.isSelectingClass = true;
		PnlClassSelect.gameObject.SetActive(true);
	}

	private void OnClassSelectHandler(string CLassName) {
		PnlCardList.gameObject.SetActive(true);
		OnClassSelected?.Invoke(CLassName);
		PnlClassSelect.gameObject.SetActive(false);
		DeckBuilderControl.isEditing = true;
		gameObject.SetActive(false);
	}

	public void OnSrlBarValueChangeHandler(float value) { // 响应滑动条滑动事件
		transform.localPosition = new Vector3(0, -770 + (EndPos - 890) * value, 0);
	}

	private void OnDeckPrevClickHandler(Transform DeckTrans) {
		SelectedCards.DeckName = DeckTrans.Find("TxtDeckName").GetComponent<TextMeshProUGUI>().text;
		Debug.Log("DeckTrans name = " + DeckTrans.Find("TxtDeckName").GetComponent<TextMeshProUGUI>().text);
		PnlCardList.gameObject.SetActive(true);
		gameObject.SetActive(false);
	}

	private void OnDeckDeleteHandler(string DeleteDeckName) {
		bool Deleted = false;
		DeckNum--;
		for(int i = 0; i < DeckListTrans.Count - 1; i++) { // 重新排序
			if(i >= DeckAssetList.Count) {
				Debug.Log("删除" + DeckListTrans[i].Find("TxtDeckName").GetComponent<TextMeshProUGUI>().text);
				Destroy(DeckListTrans[i].gameObject);
				Deleted = true;
			}                   
			else if(!Deleted && DeckAssetList[i].Order != i) {
				Debug.Log("删除" + DeckListTrans[i].Find("TxtDeckName").GetComponent<TextMeshProUGUI>().text);
				Destroy(DeckListTrans[i].gameObject);
				Deleted = true;
			}
			DeckAssetList[i].Order = i;
		}
		AssetDatabase.Refresh();
	}
}
