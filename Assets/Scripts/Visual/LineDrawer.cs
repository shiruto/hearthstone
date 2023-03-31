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
    private void Awake() {
        LineTrans = Instantiate(PfbLine, transform).transform;
        Target = LineTrans.GetChild(2);
        LR = LineTrans.GetChild(0).GetComponent<LineRenderer>();
        Arrow = LineTrans.GetChild(1);
        LineTrans.gameObject.SetActive(false);
        EventManager.AddListener(VisualEvent.DrawLine, DrawLineHandler);
    }
    private void DrawLineHandler(BaseEventArgs eventData) {
        Debug.Log("Drawing Line");
        VisualEventArgs _event = eventData as VisualEventArgs;
        Vector3 StartPos = _event.StartPos;
        Vector3 EndPos = _event.Destination;
        LineTrans.gameObject.SetActive(true);
        Target.gameObject.SetActive(false);
        Arrow.position = EndPos;
        LR.SetPosition(0, StartPos);
        LR.SetPosition(1, EndPos);
        float angle = Mathf.Atan2(EndPos.x - StartPos.x, EndPos.y - StartPos.y);
        angle *= Mathf.Rad2Deg;
        Debug.Log("Rotation = " + angle);
        Arrow.eulerAngles = new(0, 0, -angle);
    }
}
