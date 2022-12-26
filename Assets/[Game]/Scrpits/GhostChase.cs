using UnityEngine;

public class GhostChase : GhostBehavior
{
    private void OnDisable()
    {
        this.ghost.scatter.Enable();    
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 avaliableDirection in node.avaliableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(avaliableDirection.x, avaliableDirection.y, 0f);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = avaliableDirection;
                    minDistance = distance;
                }
            }
            this.ghost.movement.SetDirection(direction);
        }    
    }
}
