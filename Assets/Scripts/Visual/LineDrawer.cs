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
        EventManager.AddListener(VisualEvent.DrawCardLine, DrawCardLineHandler);
        EventManager.AddListener(VisualEvent.DrawMinionLine, DrawMinionLineHandler);
        EventManager.AddListener(VisualEvent.DeleteLine, DeleteLineHandler);
    }

    private void DrawCardLineHandler(BaseEventArgs e) {
        VisualEventArgs evt = e as VisualEventArgs;
        Debug.Log("Drawing Line");
        evt.Sender.GetComponent<CanvasGroup>().alpha = 0;
        DrawLine(evt.StartPos, evt.Destination);
    }

    private void DrawMinionLineHandler(BaseEventArgs e) {
        VisualEventArgs evt = e as VisualEventArgs;
        DrawLine(evt.StartPos, evt.Destination);
    }

    private void DrawLine(Vector3 StartPos, Vector3 EndPos) {
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
        if (e.Sender.GetComponent<CanvasGroup>()) {
            e.Sender.GetComponent<CanvasGroup>().alpha = 1;
        }
        PfbLine.SetActive(false);
    }

    public bool ifDrawTarget() { // TODO:
        return false;
    }

}
