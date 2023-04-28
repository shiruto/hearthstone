using UnityEngine;

public class ScnBattleUI : MonoBehaviour {
    private Ray ray;
    public static ScnBattleUI Instance;
    public GameObject CardPreview;
    public ICharacter Targeting = null;
    public bool isDragging;

    private void Awake() {
        Instance = this;
        Application.targetFrameRate = 10;
        EventManager.AddListener(CardEvent.OnCardPreview, OnCardPreviewHandler);
        EventManager.AddListener(CardEvent.AfterCardPreview, AfterCardPreviewHandler);
        CardPreview.SetActive(false);
    }

    private void Update() {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(Camera.main.transform.position, Input.mousePosition, Color.yellow);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, LayerMask.GetMask("UI"))) {
            Transform TargetTrans = hitInfo.collider.transform;
            if (TargetTrans.GetComponent<MinionViewController>()) {
                Targeting = TargetTrans.GetComponent<MinionViewController>().ML;
            }
            else if (TargetTrans.GetComponent<PlayerVisual>()) {
                Targeting = TargetTrans.GetComponent<PlayerVisual>().Player;
            }
            else Targeting = null;
            Debug.Log(TargetTrans.name + " ICharacter = " + Targeting);
        }
    }

    private void OnCardPreviewHandler(BaseEventArgs e) {
        if (isDragging) return;
        CardEventArgs evt = e as CardEventArgs;
        CardPreview.SetActive(true);
        CardPreview.GetComponent<BattleCardViewController>().Card = evt.Card;
        CardPreview.GetComponent<BattleCardViewController>().ReadFromAsset();
        CardPreview.transform.position = evt.Sender.transform.position - new Vector3(evt.Sender.GetComponent<RectTransform>().sizeDelta.x, 0, 0);
    }

    private void AfterCardPreviewHandler(BaseEventArgs e) {
        CardPreview.SetActive(false);
    }

}
