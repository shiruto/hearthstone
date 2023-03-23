using System;
using UnityEngine;

public class Draggable : MonoBehaviour {
    #region variables
    public bool canPreview;
    private GameObject CardPreview;
    public bool canDrag = false;
    private bool isDragging = false;
    private Vector3 StartPos;
    Vector3 Distance;
    public GameObject PfbCard;
    public static event Action<Transform, Vector3> OnCardReturn;
    public static event Action<Transform> OnCardUse;
    public event Action<Vector3> DrawLine;
    public bool ifDrawLine;
    #endregion
    private void OnMouseEnter() {
        if (canPreview && !isDragging) {
            CardPreview = Instantiate(PfbCard, transform.parent.parent);
            Destroy(CardPreview.GetComponent<BoxCollider>());
            CardPreview.GetComponent<CardManager>().cardAsset = GetComponent<CardManager>().cardAsset;
            CardPreview.transform.position = new Vector3(0, 100, 0) + transform.position;
            CardPreview.transform.localScale = new(2f, 2f, 2f);
            CardPreview.GetComponent<CardManager>().ReadFromAsset();
        }
    }
    private void OnMouseExit() {
        if (canPreview) {
            Destroy(CardPreview);
        }
    }
    private void OnMouseDown() {
        if (canDrag) {
            if (!isDragging) StartPos = transform.position;
            Debug.Log(StartPos);
            Distance = Input.mousePosition - transform.position;
        }
        if (canPreview) {
            Debug.Log(transform.name);
            transform.localScale = 2 * Vector3.one;
            Destroy(CardPreview);
        }
    }
    private void OnMouseDrag() {
        if (canDrag) {
            isDragging = true;
            if (ifDrawLine && Input.mousePosition.y > 200) {
                DrawLine?.Invoke(Input.mousePosition);
            }
            else {
                transform.Find("PfbLine(Clone)").gameObject.SetActive(false);
                transform.position = Input.mousePosition - Distance;
            }
        }
    }

    private void OnMouseUp() {
        if (Input.mousePosition.y > 200 && canDrag) {
            OnCardUse?.Invoke(transform);
        }
        else {
            OnCardReturn?.Invoke(transform, StartPos);
            isDragging = false;
        }
        if (ifDrawLine) {
            transform.Find("PfbLine(Clone)").gameObject.SetActive(false);
        }
        if (canPreview) {
            transform.localScale = Vector3.one;
        }

    }
}