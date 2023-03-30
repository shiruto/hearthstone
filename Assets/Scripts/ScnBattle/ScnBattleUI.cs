using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScnBattleUI : MonoBehaviour {
    RaycastHit hitInfo = new();
    private List<ReturningCardInfo> ReturningCards = new();
    public static event Action<CardAsset> OnCardUse;
    class ReturningCardInfo {
        public Transform card;
        public Vector3 ReturnPos;
        public ReturningCardInfo(Transform card, Vector3 ReturnPos) {
            this.card = card;
            this.ReturnPos = ReturnPos;
        }
    }
    private Ray ray;
    private void Awake() {
        Application.targetFrameRate = 60;

        Draggable.OnCardReturn += OnCardReturnHandler;
    }
    private void Update() {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(Camera.main.transform.position, Input.mousePosition, Color.yellow);
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("UI"))) {
            // BattleControl.Instance.Targeting = hitInfo.collider.GetComponent<ICharacter>();
        }
        if (ReturningCards.Any()) { // 非空
            foreach (var cardInfo in ReturningCards) {
                cardInfo.card.position -= GetSpeed(Vector3.Magnitude(cardInfo.card.position - cardInfo.ReturnPos)) * Time.deltaTime * Vector3.Normalize(cardInfo.card.position - cardInfo.ReturnPos);
                if (Vector3.Magnitude(cardInfo.card.position - cardInfo.ReturnPos) < 10) { // 距离足够近时不再移动
                    cardInfo.card.position = cardInfo.ReturnPos;
                    ReturningCards.Remove(cardInfo);
                }
            }
        }
    }

    private void OnCardReturnHandler(Transform card, Vector3 ReturnPos) {
        if (!ReturningCards.Exists((ReturningCardInfo a) => a.card.Equals(card))) {
            ReturningCardInfo ReturningCard = new(card, ReturnPos);
            ReturningCards.Add(ReturningCard);
        }
    }
    private float GetSpeed(float dif) { // 速度函数
        if (dif > 300) {
            return 1200;
        }
        else {
            return dif * dif / 75 + 400;
        }
    }
}
