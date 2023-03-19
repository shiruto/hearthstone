using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckControl: MonoBehaviour {
	public static event Action<CardAsset> OnDraw;
	private List<CardAsset> Deck = new();
	private enum Sum {
		Empty,
		LastOne,
		Less,
		Medium,
		Alot,
		Full
	}

	private void DrawCard() {
		OnDraw?.Invoke(RemoveCard(0));
	}

	private CardAsset RemoveCard(int index) {
		CardAsset Card = Deck[index - 1];
		Deck.RemoveAt(index - 1);
		return Card;
	}

	private void Sort() {
		Deck.Sort((CardAsset a, CardAsset b) => {
			return a.ManaCost.CompareTo(b.ManaCost);
		});
	}

	private void AddCard(CardAsset Card) {
		Deck.Add(Card);
	}

}
