using UnityEngine;

public class VisualEventArgs : BaseEventArgs {
    public Vector3 StartPos;
    public bool DrawTarget;

    public VisualEventArgs CreateEventArgs(VisualEvent _eventType, GameObject _sender, Vector3 _start = default, bool _drawTarget = false) {
        CreateEventArgs(_eventType, _sender, null);
        StartPos = _start;
        DrawTarget = _drawTarget;
        return this;
    }

}