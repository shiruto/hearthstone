using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour {
    private LineRenderer LR;
    private Transform Target;
    private Transform Arrow;
    private Transform LineTrans;
    public GameObject PfbLine;
    public event Func<bool> isTarget;

    private void Awake() {
        LineTrans = Instantiate(PfbLine, transform).transform;
        Target = LineTrans.GetChild(2);
        LR = LineTrans.GetChild(0).GetComponent<LineRenderer>();
        Arrow = LineTrans.GetChild(1);
        LineTrans.gameObject.SetActive(false);

        GetComponent<Draggable>().DrawLine += DrawLineHandler;
    }

    private void DrawLineHandler(Vector3 EndPos) {
        LineTrans.gameObject.SetActive(true);
        Target.gameObject.SetActive(false);

        Arrow.position = EndPos;
        LR.SetPosition(1, EndPos);
        float angle = Mathf.Atan2(EndPos.x - 960, EndPos.y - 250);
        angle *= Mathf.Rad2Deg;
        Debug.Log("Rotation = " + angle);
        Arrow.eulerAngles = new(0, 0, -angle);
        // if (isTarget.Invoke()) {
        //     Target.gameObject.SetActive(true);
        // }
    }
}
