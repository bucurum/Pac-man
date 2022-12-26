using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> avaliableDirections {get; private set;}
    public LayerMask obstacleLayerMask;

    void Start()
    {
        this.avaliableDirections = new List<Vector2>();

        CheckAvaliableDirection(Vector2.up);
        CheckAvaliableDirection(Vector2.down);
        CheckAvaliableDirection(Vector2.left);
        CheckAvaliableDirection(Vector2.right);
    }

    private void CheckAvaliableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * .5f, 0f, direction, 1f, this.obstacleLayerMask);

        if (hit.collider == null)
        {
            this.avaliableDirections.Add(direction);
        }
    }
}
