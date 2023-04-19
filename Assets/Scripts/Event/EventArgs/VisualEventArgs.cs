using UnityEngine;

public class VisualEventArgs : BaseEventArgs {
    public Vector3 StartPos;
    public Vector3 Destination;
    public bool DrawTarget;
    public VisualEventArgs CreateEventArgs(VisualEvent _eventType, GameObject _sender, Vector3 _start, Vector3 _destination, bool _drawTarget = false) {
        CreateEventArgs(_eventType, _sender, null);
        StartPos = _start;
        Destination = _destination;
        DrawTarget = _drawTarget;
        return this;
    }
}