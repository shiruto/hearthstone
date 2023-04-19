using UnityEngine;

public class StarterOptionController : MonoBehaviour {
    public Transform CancelTrans;
    public bool IsCancelled;

    private void Awake() {
        IsCancelled = false;
    }

    private void OnMouseDown() {
        IsCancelled = !IsCancelled;
        CancelTrans.gameObject.SetActive(IsCancelled);
    }
}
