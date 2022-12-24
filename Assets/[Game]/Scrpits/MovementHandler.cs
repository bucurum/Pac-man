using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementHandler : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] float speedMultiplier = 1f;
    [SerializeField] Vector2 initalDirection;
    [SerializeField] LayerMask obstacleLayerMask;
    public Rigidbody2D rb2d {get; private set;}
    public Vector2 direction {get; private set;}
    public Vector2 nextDirection {get; private set;}
    
    void Awake()
    {
        this.rb2d = this.GetComponent<Rigidbody2D>();
    }



}
