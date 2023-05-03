using System.Collections;
using UnityEngine;

public class LineDrawer : MonoBehaviour {
    private LineRenderer LR;
    private Transform Target;
    private Transform Arrow;
    [SerializeField]
    private GameObject PfbLine;
    [HideInInspector]
    public static LineDrawer Instance;
    public bool isDrawing = false;
    private GameObject DrawingObj;

    private void Awake() {
        Instance = this;
        Target = PfbLine.transform.GetChild(2);
        LR = PfbLine.transform.GetChild(0).GetComponent<LineRenderer>();
        Arrow = PfbLine.transform.GetChild(1);
        PfbLine.SetActive(false);
        EventManager.AddListener(VisualEvent.DrawLine, DrawLineHandler);
        EventManager.AddListener(VisualEvent.DeleteLine, DeleteLineHandler);
    }

    private void DrawLineHandler(BaseEventArgs e) {
        VisualEventArgs evt = e as VisualEventArgs;
        DrawingObj = evt.Sender;
        StartCoroutine(DrawLineCoroutine(evt.StartPos));
    }

    private IEnumerator DrawLineCoroutine(Vector3 StartPos) {
        Debug.Log("Drawing Line");
        isDrawing = true;
        PfbLine.SetActive(true);
        DrawTarget();
        LR.SetPosition(0, StartPos);
        while (isDrawing) {
            Vector3 EndPos = Input.mousePosition;
            Arrow.position = EndPos;
            Target.position = EndPos;
            LR.SetPosition(1, EndPos);
            Arrow.eulerAngles = new(0, 0, -Mathf.Atan2(EndPos.x - StartPos.x, EndPos.y - StartPos.y) * Mathf.Rad2Deg);
            yield return null;
        }
        PfbLine.SetActive(false);
    }

    private void DeleteLineHandler(BaseEventArgs e) {
        isDrawing = false;
    }

    public void DrawTarget() {
        if (DrawingObj.GetComponent<BattleCardViewController>()) {
            CardBase DrawingCard = DrawingObj.GetComponent<BattleCardViewController>().Card;
            Target.gameObject.SetActive((DrawingCard as ITarget).Match(ScnBattleUI.Instance.TargetCharacter) && DrawingCard.CanBeTarget(ScnBattleUI.Instance.TargetCharacter));
        }
        else if (DrawingObj.GetComponent<MinionViewController>()) {
            Target.gameObject.SetActive(DrawingObj.GetComponent<MinionViewController>().ML.ValidTarget(ScnBattleUI.Instance.TargetCharacter));
        }
        else if (DrawingObj.GetComponent<PlayerVisual>()) {
            Target.gameObject.SetActive(DrawingObj.GetComponent<PlayerVisual>().ValidTarget(ScnBattleUI.Instance.TargetCharacter));
        }
        else {
            Debug.LogWarning("Wrong Dragging GameObject");
        }
    }

}
