using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour {
    private LineRenderer LR;
    private Transform Target;
    private Transform Arrow;

    private void Awake() {
        Target = transform.GetChild(2);
        LR = transform.GetChild(0).GetComponent<LineRenderer>();
        Arrow = transform.GetChild(1);

        Target.gameObject.SetActive(false);

        ScnBattleUI.DrawLine += DrawLineHandler;
    }

    private void DrawLineHandler(Vector3 EndPos){
        Vector3 MousePos = Input.mousePosition;
        Arrow.position = EndPos;
        LR.SetPosition(1, MousePos);
        float angle = Mathf.Atan2(MousePos.x, MousePos.y + 270);
        Arrow.Rotate(new(0, 0, -angle), Space.Self);
    }
}
