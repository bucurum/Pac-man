using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    public bool eaten {get; private set;}

    public override void Enable(float duration)
    {
        base.Enable(duration);

        this.body.enabled = false;
        this.eyes.enabled = false;
        this.blue.enabled = true;
        this.white.enabled = false;

        Invoke(nameof(Flash), duration / 2);
    }
    public override void Disable()
    {
        base.Disable();

        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    private void Eaten()
    {
        this.eaten = true;
        Vector3 position = this.ghost.home.inside.position;
        position.z = this.ghost.transform.position.z;
        this.ghost.transform.position = position;

        this.ghost.home.Enable(this.duration);

        this.body.enabled = false;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;

    }
    private void Flash() 
    {
        if (!eaten)
        {
            this.blue.enabled = false;
            this.white.enabled = true;    
            this.white.GetComponent<AnimatedSprite>().Restart();
        }
        
    }

    private void OnEnable() 
    {
        this.ghost.movement.speedMultiplier = .5f; 
        this.eaten = false;

    }
    private void OnDisable() 
    {
        this.ghost.movement.speedMultiplier = 1f;
        this.eaten = false;

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.enabled)
            {
                Eaten();
            }  
        }
            
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 avaliableDirection in node.avaliableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(avaliableDirection.x, avaliableDirection.y, 0f);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance && avaliableDirection != (-this.ghost.movement.direction))
                {
                    direction = avaliableDirection;
                    maxDistance = distance;
                }
            }
            this.ghost.movement.SetDirection(direction);
        }    
    }

}
