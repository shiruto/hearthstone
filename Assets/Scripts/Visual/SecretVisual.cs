using System.Collections.Generic;
using UnityEngine;

public class SecretVisual : MonoBehaviour {
    public List<Transform> Secrets = new(5);
    public SecretLogic sl;

    private void Awake() {
        foreach (Transform secretTrans in transform) {
            Secrets.Add(secretTrans);
        }
        EventManager.AddListener(EmptyParaEvent.SecretVisualUpdate, SecretVisualUpdateHandler);
    }

    private void SecretVisualUpdateHandler(BaseEventArgs e) {
        for (int i = 0; i < 5; i++) {
            if (i < sl.secrets.Count) {
                Secrets[i].gameObject.SetActive(true);
                Secrets[i].GetComponent<SecretViewController>().Secret = sl.secrets[i];
                Secrets[i].GetComponent<SecretViewController>().UpdateSecretColor();
            }
            else {
                Secrets[i].gameObject.SetActive(false);
            }
        }
    }

}