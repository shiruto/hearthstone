using UnityEngine;
using UnityEditor;

static class CardUnityIntegration {
	[MenuItem("Assets/Create/CardAsset")]
	public static void CreateYourScriptableObject() {
		ScriptableObjectUtility2.CreateAsset<CardAsset>();
	}
	[MenuItem("Assets/Create/DeckAsset")]
	public static void CreateScriptableDeck() {
		ScriptableObjectUtility2.CreateAsset<DeckAsset>();
	}

}