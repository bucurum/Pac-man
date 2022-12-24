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
    public Vector2 startPosition {get; private set;}

    void Awake()
    {
        this.rb2d = this.GetComponent<Rigidbody2D>();
        this.startPosition = this.transform.position;
    }

    void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.speedMultiplier = 1f;
        this.direction = this.initalDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startPosition;
        this.rb2d.isKinematic = false;
        this.enabled = true;
    }

    void FixedUpdate() //Physics is needed to be fixedupdate
    {
        Vector2 position = this.rb2d.position;
        Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
        this.rb2d.MovePosition(position + translation);
    }
    void Update()
    {
        if (this.nextDirection != Vector2.zero)
        {
            SetDirection(this.nextDirection);
        }
    }
    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        }
        else
        {
            this.nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * .75f, 0f, direction, 1.5f, this.obstacleLayerMask);
        return hit.collider != null;
    }
}
