using System;
using UnityEngine;

public class ScnBattleUI : MonoBehaviour {
    RaycastHit hitInfo = new();
    public static event Action<Vector3> DrawLine;
    private void Awake() {
        Application.targetFrameRate = 60;
    }
    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(Camera.main.transform.position, Input.mousePosition, Color.yellow);
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("UI"))) {
            //Debug.Log($"{hitInfo.collider.name}");
        }
    }
}
