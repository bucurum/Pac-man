using UnityEngine;

public class PowerPellet : Pellet //this class is inherit in Pellet class
{
    public float duration = 8f;
    protected override void Eat() //protected cause powerpallet is our sub classes, if we define that private, powerpallet class can`t be able to acces this function
    {
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
    }
}
