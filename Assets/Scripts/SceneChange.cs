using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange: MonoBehaviour {
	// Start is called before the first frame update
	public string tarScene;
	void Start() {
		GetComponent<Button>().onClick.AddListener(changeScene);
	}

	// Update is called once per frame
	void Update() {

	}

	void changeScene() {
		SceneManager.LoadScene(tarScene);
	}
}
