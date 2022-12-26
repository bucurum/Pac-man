using UnityEngine;

public class Ghost : MonoBehaviour
{
    public MovementHandler movement {get; private set;}
    public GhostHome home {get; private set;}
    public GhostChase chase {get; private set;}
    public GhostFrightened frightened {get; private set;}
    public GhostScatter scatter {get; private set;}
    public GhostBehavior initalBehavior;
    public Transform target;
    public int points = 200;

    void Awake()
    {
        this.movement = GetComponent<MovementHandler>();
        this.home = GetComponent<GhostHome>();
        this.chase = GetComponent<GhostChase>();
        this.frightened = GetComponent<GhostFrightened>();
        this.scatter = GetComponent<GhostScatter>();
    }

    void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.movement.ResetState();
        this.gameObject.SetActive(true);
    
        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Disable();

        if (this.home != this.initalBehavior)
        {
            this.home.Disable();
        }
        if (this.initalBehavior != null)
        {
            this.initalBehavior.Enable();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }    
        }
            
    }

}
