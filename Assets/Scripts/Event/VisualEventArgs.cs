using UnityEngine;

public class VisualEventArgs : BaseEventArgs {
    public Vector3 StartPos;
    public Vector3 EndPos;
    public VisualEventArgs CreateEventArgs(VisualEvent _eventType, GameObject _sender, Vector3 _start, Vector3 _end) {
        base.CreateEventArgs(_eventType, _sender);
        StartPos = _start;
        EndPos = _end;
        return this;
    }
}