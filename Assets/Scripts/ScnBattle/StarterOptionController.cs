using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterOptionController : MonoBehaviour {
    Transform CancelTrans;
    private bool _isCancelled;
    public bool IsCancelled {
        get {
            _isCancelled = !_isCancelled;
            return _isCancelled;
        }
    }

    private void Awake() {
        CancelTrans = transform.Find("Cancel");
        _isCancelled = false;
    }

    private void OnMouseDown() {
        CancelTrans.gameObject.SetActive(IsCancelled);
    }
}
