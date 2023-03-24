using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDFactory {
    public static int IDNum;
    public static int GetID() {
        IDNum++;
        return IDNum;
    }

    public static void ClearID() {
        IDNum = 0;
    }
}
