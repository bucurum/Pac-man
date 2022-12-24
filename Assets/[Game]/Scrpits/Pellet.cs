using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10;

    protected virtual void Eat() //protected cause powerpallet is our sub classes, if we define that private, powerpallet class can`t be able to acces this function
    {
        FindObjectOfType<GameManager>().PelletEaten(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Packman"))
        {
            Eat();    
        }
    }
    

}
