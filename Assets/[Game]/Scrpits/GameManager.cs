using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public int score {get; private set;}   //public getter, private setter
    public int lives {get; private set;}

    void Start()
    {
        NewGame();
    }
    void Update()
    {
        if (Input.anyKeyDown && this.lives > 0)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }
    private void NewRound()
    {
        foreach(Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void ResetState()
    {
        for(int i = 0; i < this.ghosts.Length ; i++)
        {
            this.ghosts[i].gameObject.SetActive(true);
        }
        this.pacman.gameObject.SetActive(true);
    }

    private void SetScore(int score)
    {
       this.score = score;
    }
    private void SetLives(int lives)
    {
        this.lives = lives;
    }
    private void GameOver()
    {
        for(int i = 0 ; i < this.ghosts.Length ; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false);
    }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(this.score + ghost.points);
    }
    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);

        SetLives(this.lives - 1);
        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3f);  //Invoke is called the method after some seconds which you setted time before.
        }
        else
        {
            GameOver();
        }

    }
}
