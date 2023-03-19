using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardFrameManager : MonoBehaviour {
    public TextMeshProUGUI TxtCardNum;

    public void AddCardNum(int CardNum) {
        TxtCardNum.text = "" + CardNum;
    }
}
