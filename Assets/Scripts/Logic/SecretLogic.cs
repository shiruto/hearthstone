using System.Collections.Generic;
using UnityEngine;

public class SecretLogic {
    public List<SecretCard> secrets = new(5);
    public PlayerLogic Owner;

    public void AddSecret(SecretCard SC) {
        if (secrets.Exists((SecretCard a) => a.CA.Equals(SC.CA))) {
            Debug.Log("Secret already existed");
            return;
        }
        if (secrets.Count == 5) {
            Debug.Log("too many secrets");
            return;
        }
        secrets.Add(SC);
        EventManager.AddListener(SC.trigger.eventType, SC.trigger.callback);
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.SecretVisualUpdate).Invoke();
    }

    public void RemoveSecret(SecretCard SC) {
        secrets.Remove(SC);
        EventManager.DelListener(SC.trigger.eventType, SC.trigger.callback);
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.SecretVisualUpdate).Invoke();
    }

    public void RemoveAllSecret() {
        foreach (SecretCard sc in secrets) {
            secrets.Remove(sc);
            EventManager.DelListener(sc.trigger.eventType, sc.trigger.callback);
        }
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.SecretVisualUpdate).Invoke();
    }

}