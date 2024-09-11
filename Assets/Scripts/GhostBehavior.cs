using System;
using UnityEngine;

[RequireComponent(typeof(Ghost))]
public class GhostBehavior : MonoBehaviour
{
    public Ghost ghosts { get; private set; }

    public float duration;
    private void Awake()
    {
        this.ghosts = GetComponent<Ghost>();
        this.enabled = false;
    }

    public void Enable()
    {
        Enable(this.duration);
    }

    public virtual void Enable(float duration)
    {
        this.enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke();
    }
}
