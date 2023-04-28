using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeckBuilderControl : MonoBehaviour {
    public static bool isEditing = false;
    public static bool isSelectingClass = false;
    public static event Action<string> OnClassFilter;
    public static event Action<Transform> OnCardClick;
    public static event Action<Transform> OnCardPrevClick;
    public static event Action<Transform> OnDeckPrevClick;
    public static event Action OnExitEditing;
    public static event Action<string> OnClassSelect;
    public static event Action OnNewDeckCancel;
    public static event Action<CardAsset, Vector3> OnHoverAboveCard;
    public static event Action<CardAsset> OnCardSearch;
    private Transform PnlClassSelect;
    private string SelectedClass = "";

    RaycastHit hit = new();
    Ray ray;
    private void Awake() {
        Application.targetFrameRate = 60;
    }

    private void Start() {
        PnlClassSelect = GameObject.Find("PnlClassSelect").transform;
        PnlClassSelect.gameObject.SetActive(false);
        GameObject.Find("DeckSrlBar").SetActive(false);
    }
    private void Update() {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(Camera.main.transform.position, Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("UI"))) {
            //string name = hit.collider.name;
            //Debug.Log(name);
            GameObject go = hit.collider.gameObject;
            if (Input.GetMouseButtonDown(0)) {
                if (go.GetComponent<Button>()) {
                    Debug.Log("Button down");
                    if (go.name == "BtnReturn") {
                        if (isEditing) {
                            isEditing = false;
                            OnExitEditing?.Invoke();
                        }
                        else if (isSelectingClass) {
                            isSelectingClass = false;
                            isEditing = false;
                            OnNewDeckCancel?.Invoke();
                        }
                        else {
                            SceneManager.LoadScene("ScnModeSelect");
                        }
                    }
                    if (go.name == "BtnConfirm" && isSelectingClass) {
                        isEditing = true;
                        OnClassSelect?.Invoke(SelectedClass);
                        isSelectingClass = false;
                    }
                    else if (Enum.IsDefined(typeof(ClassType), go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text)) {
                        // Class Filter On
                        OnClassFilter?.Invoke(go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
                    }
                }
                else if (isEditing && go.GetComponent<CardViewController>()) {
                    Debug.Log("Selecting card");
                    OnCardClick?.Invoke(go.transform);
                }
                else if (isEditing && go.GetComponent<CardPrevManager>()) {
                    Debug.Log("dicard card");
                    OnCardPrevClick?.Invoke(go.transform);
                }
                else if (!isEditing && go.GetComponent<DeckPrevManager>()) {
                    Debug.Log("Selecting deck");
                    isEditing = true;
                    OnDeckPrevClick?.Invoke(go.transform);
                }
                else if (isSelectingClass && Enum.IsDefined(typeof(ClassType), go.name)) {
                    Debug.Log("Selecting class");
                    PnlClassSelect.Find("Selected").GetComponent<Image>().sprite = go.GetComponent<Image>().sprite;
                    SelectedClass = go.name;
                }
            }
            if (go.GetComponent<CardPrevManager>()) {
                OnHoverAboveCard?.Invoke(go.GetComponent<CardPrevManager>().cardAsset, go.transform.position);
            }
            if (Input.GetMouseButtonDown(1) && isEditing && go.GetComponent<CardPrevManager>()) {
                OnCardSearch?.Invoke(go.GetComponent<CardPrevManager>().cardAsset);
            }
        }

    }
}
