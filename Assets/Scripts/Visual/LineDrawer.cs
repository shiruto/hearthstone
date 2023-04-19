using UnityEngine;

public class LineDrawer : MonoBehaviour {
    private LineRenderer LR;
    private Transform Target;
    private Transform Arrow;
    private Transform LineTrans;
    public GameObject PfbLine;

    private void Awake() {
        LineTrans = PfbLine.transform;
        Target = LineTrans.GetChild(2);
        LR = LineTrans.GetChild(0).GetComponent<LineRenderer>();
        Arrow = LineTrans.GetChild(1);
        LineTrans.gameObject.SetActive(false);
        EventManager.AddListener(VisualEvent.DrawLine, DrawLineHandler);
        EventManager.AddListener(VisualEvent.DeleteLine, DeleteLineHandler);
    }

    private void DrawLineHandler(BaseEventArgs eventData) {
        VisualEventArgs _event = eventData as VisualEventArgs;
        HideCard(_event.Sender, true);
        Vector3 StartPos = _event.StartPos;
        Vector3 EndPos = _event.Destination;
        LineTrans.gameObject.SetActive(true);
        Target.gameObject.SetActive(false); // TODO: Draw Target
        Arrow.position = EndPos;
        LR.SetPosition(0, StartPos);
        LR.SetPosition(1, EndPos);
        float angle = Mathf.Atan2(EndPos.x - StartPos.x, EndPos.y - StartPos.y);
        angle *= Mathf.Rad2Deg;
        Arrow.eulerAngles = new(0, 0, -angle);
    }

    private void DeleteLineHandler(BaseEventArgs e) {
        HideCard(e.Sender, false);
        PfbLine.SetActive(false);
    }

    private void HideCard(GameObject CardObj, bool hide) {
        foreach (Transform child in CardObj.transform) {
            child.gameObject.SetActive(!hide);
        }
    }

    public bool ifDrawTarget() {
        return false;
    }

}
