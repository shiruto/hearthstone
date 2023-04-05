using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour {
    public string TargetScene;

    void Start() {
        GetComponent<Button>().onClick.AddListener(ChangeScene);
    }

    void ChangeScene() {
        if (TargetScene == null) {
            Debug.Log("Empty Scene Name!");
            return;
        }
        SceneManager.LoadScene(TargetScene);
    }
}
