using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDContainer : MonoBehaviour {
    public int ID;
    private static List<IDContainer> allIDHolders = new();

    void Awake() {
        allIDHolders.Add(this);
    }

    public static GameObject GetGameObjectWithID(int ID) {
        foreach (IDContainer i in allIDHolders) {
            if (i.ID == ID)
                return i.gameObject;
        }
        return null;
    }

    public static void ClearIDHoldersList() {
        allIDHolders.Clear();
    }
}
