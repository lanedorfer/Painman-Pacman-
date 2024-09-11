using UnityEngine;

public class GhostChase : GhostBehavior
{
    private void OnDisable()
    {
        this.ghosts.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Node node = other.GetComponent<Node>();
        if (node !=null && this.enabled && !this.ghosts.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistace = float.MaxValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (this.ghosts.target.position - newPosition).sqrMagnitude;

                if (distance < minDistace)
                {
                    direction = availableDirection;
                    minDistace = distance;
                }
            }
            this.ghosts.movement.SetDirection(direction);
        }
    }
}
