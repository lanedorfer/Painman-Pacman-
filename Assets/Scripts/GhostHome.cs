using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostHome : GhostBehavior
{
    public Transform inside;
    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    public void OnDisable()
    {
        if (this.gameObject.activeSelf)
        { 
            StartCoroutine(ExitTransition());   
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.ghosts.movement.SetDirection(-this.ghosts.movement.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        this.ghosts.movement.SetDirection(Vector2.up, true);
        this.ghosts.movement.rigidbody.isKinematic = true;
        this.ghosts.movement.enabled = false;

        Vector3 position = this.transform.position;

        float duration = 0.5f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghosts.transform.position = newPosition;
            yield return null;
        }

        elapsed = 0.0f;
        
        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghosts.transform.position = newPosition;
            yield return null;
        }

        this.ghosts.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        this.ghosts.movement.rigidbody.isKinematic = false;
        this.ghosts.movement.enabled = true;
    }
    
}
