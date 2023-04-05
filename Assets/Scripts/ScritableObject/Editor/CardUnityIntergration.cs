using UnityEditor;
using UnityEngine;

static class CardUnityIntegration {
    [MenuItem("Assets/Create/MinionCardAsset")]
    public static void CreateMinionCardAsset() {
        ScriptableObjectUtility2.CreateAsset<SpellCardAsset>();
    }

    [MenuItem("Assets/Create/SpellCardAsset")]
    public static void CreateSpellCardAsset() {
        ScriptableObjectUtility2.CreateAsset<MinionCardAsset>();
    }

    [MenuItem("Assets/Create/DeckAsset")]
    public static void CreateScriptableDeck() {
        ScriptableObjectUtility2.CreateAsset<DeckAsset>();
    }

}