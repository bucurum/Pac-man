using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer {get; private set;}
    public Sprite[] sprites;
    [SerializeField] float animationTime = .25f;
    public int animationFrame {get; private set;}
    [SerializeField] bool loop = true;

    void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        InvokeRepeating(nameof(Advance), animationTime, animationTime);
    }

    private void Advance()
    {
        if (!this.spriteRenderer.enabled) //if sprite renderer disabled, return
        {
            return;
        }
        this.animationFrame++;

        if (animationFrame >= this.sprites.Length && this.loop)
        {
            this.animationFrame = 0;
        }
        if (this.animationFrame >=0 && this.animationFrame < this.sprites.Length)
        {
            this.spriteRenderer.sprite = this.sprites[this.animationFrame];
        }
    }

    public void Restart()
    {
        this.animationFrame = -1;
        Advance();
    }
}
