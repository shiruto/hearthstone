using UnityEngine;

public class BattleControl {
    private BattleControl() {

    }
    private static BattleControl instance;
    public static BattleControl Instance() {
        instance ??= new();
        return instance;
    }
}