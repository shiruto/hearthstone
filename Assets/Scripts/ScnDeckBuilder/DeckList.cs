using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckList: MonoBehaviour {
	public static event Action<string> OnClassSelected;
	public static int DeckNum = 0;

	public GameObject PfbDeckPrev;
	private Transform SrlBar;
	private Transform PnlCardList;
	private Transform PnlDeckList;
	private Transform PnlClassSelect;
	private Transform BtnNewDeck;
	private List<Transform> DeckListTrans = new();
	private List<DeckAsset> DeckAssetList;
	
	private readonly int MaxDeckNum = 27;
	private readonly int MaxDeckContain = 9; // 无滚动条最大卡组数量
	private readonly int StartPosY = 1215;
	private readonly int DeckPrevHeight = 90;
	private int EndPos;

	private void Awake() {
		DeckBuilderControl.OnDeckPrevClick += OnDeckPrevClickHandler;
		DeckBuilderControl.OnClassSelect += OnClassSelectHandler;

		SrlBar = GameObject.Find("DeckSrlBar").transform;
		PnlCardList = GameObject.Find("PnlCardList").transform;
		PnlDeckList = GameObject.Find("MaskDeckList/PnlDeckList").transform;
		PnlClassSelect = GameObject.Find("PnlClassSelect").transform;
		BtnNewDeck = GameObject.Find("BgNewDeck").transform;
		DeckAssetList = new List<DeckAsset>(Resources.LoadAll<DeckAsset>("ScriptableObject/Deck"));
		DeckNum = DeckAssetList.Count;

		if(DeckNum > MaxDeckContain) {
			SrlBar.gameObject.SetActive(true);
		}

		LoadDeck();
	}

	private void LoadDeck() {
		Transform newDeckTrans;
		int i = 0;
		foreach(DeckAsset DA in DeckAssetList) {
			newDeckTrans = Instantiate(PfbDeckPrev, PnlDeckList).transform;
			newDeckTrans.name = "Deck" + i;
			newDeckTrans.localPosition = new Vector3(0, StartPosY - DeckPrevHeight / 2 - DeckPrevHeight * i, 0);
			DeckListTrans.Add(newDeckTrans);
			DeckListTrans[i].Find("TxtDeckName").GetComponent<TextMeshProUGUI>().text = DA.name;
			i++;
		}
		if(DeckNum < MaxDeckNum) {
			BtnNewDeck.localPosition = new Vector3(0, StartPosY - DeckPrevHeight / 2 - DeckPrevHeight * i, 0);
			DeckListTrans.Add(BtnNewDeck);
		}
		if(DeckNum > MaxDeckContain) {
			SrlBar.gameObject.SetActive(true);
		}
		EndPos = i * DeckPrevHeight;
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
}
