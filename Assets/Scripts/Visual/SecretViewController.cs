using UnityEngine;
using UnityEngine.UI;

public class SecretViewController : MonoBehaviour {
    public Image SecretBG;
    public SecretCard Secret;

    private void OnMouseEnter() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardPreview, gameObject, null, Secret).Invoke();
    }

    private void OnMouseExit() {
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.AfterCardPreview, gameObject, null, Secret).Invoke();
    }

    public void UpdateSecretColor() {
        GameData.SecretColor.TryGetValue(Secret.CA.ClassType, out Color secretColor);
        if (secretColor != null) {
            SecretBG.color = secretColor;
        }
        else {
            Debug.Log("Wrong Secret Class");
        }
    }

}