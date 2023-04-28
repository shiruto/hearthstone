using System.Collections.Generic;
using UnityEngine;

public class SecretLogic {
    public List<SecretCard> secrets = new(5);

    public void RemoveSecret(SecretCard sc) {
        secrets.Remove(sc);
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.SecretVisualUpdate).Invoke();
    }

    public void AddSecret(SecretCard SC) {
        if (secrets.Exists((SecretCard a) => a.CA.Equals(SC.CA))) {
            Debug.Log("Secret already existed");
            return;
        }
        if (secrets.Count == 5) {
            Debug.Log("too many secres");
            return;
        }
        SC.AddTrigger();
        secrets.Add(SC);
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.SecretVisualUpdate).Invoke();
    }

}