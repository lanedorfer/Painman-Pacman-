using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        this.ghosts.chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Node node = other.GetComponent<Node>();
        if (node !=null && this.enabled && !this.ghosts.frightened.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);
            if (node.availableDirections[index] == -this.ghosts.movement.direction && node.availableDirections.Count > 1)
            {
                index++;

                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }
            this.ghosts.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
