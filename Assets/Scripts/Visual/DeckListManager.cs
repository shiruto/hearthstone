using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DeckListManager : MonoBehaviour {
    public List<DeckAsset> Decks;
    public List<Transform> DeckTrans;
    public Transform BtnNextPage;
    public Transform BtnLastPage;
    public Transform TxtPage;
    public int curPageNum;
    public int maxPageNum;
    private void Awake() {
        Decks = Resources.LoadAll("ScriptableObject/Deck").Cast<DeckAsset>().ToList();
        foreach (Transform deck in transform) {
            DeckTrans.Add(deck);
        }
        maxPageNum = Decks.Count / 9 + ((Decks.Count % 9 == 0) ? 0 : 1);
        curPageNum = 0;
    }
    private void Start() {
        RefreshDeck();
    }
    private void RefreshDeck() {
        BtnLastPage.gameObject.SetActive(curPageNum == 0);
        BtnNextPage.gameObject.SetActive(curPageNum + 1 == maxPageNum);
        for (int i = 0; i < 9; i++) {
            if (i + curPageNum * 9 < Decks.Count) {
                DeckTrans[i].gameObject.SetActive(true);
                DeckTrans[i].GetComponent<DeckPrevManager>().DA = Decks[i + curPageNum * 9];
                DeckTrans[i].GetComponent<DeckPrevManager>().ReadFromAsset();
            }
            else DeckTrans[i].gameObject.SetActive(false);
        }
        TxtPage.GetComponent<TextMeshProUGUI>().text = curPageNum + 1 + "/" + maxPageNum;
    }

    public void NextPage() {
        curPageNum++;
        RefreshDeck();
    }

    public void LastPage() {
        curPageNum--;
        RefreshDeck();
    }
}
