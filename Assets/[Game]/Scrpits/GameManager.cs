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
    public int ghostsMultiplier {get; private set;} = 1;

    void Start()
    {
        NewGame();
    }
    void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown)
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
        ResetGhostMultiplier();
        for(int i = 0; i < this.ghosts.Length ; i++)
        {
            this.ghosts[i].ResetState();
        }
        this.pacman.ResetState();
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
        SetScore(this.score + ghost.points * this.ghostsMultiplier);
        this.ghostsMultiplier++;
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
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }
    public void PowerPelletEaten(PowerPellet powerPellet)
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(powerPellet.duration);
        }

        PelletEaten(powerPellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), powerPellet.duration);

        

    }
    private void ResetGhostMultiplier()
    {
        this.ghostsMultiplier = 1;
    }

    private bool HasRemainingPellets()
    {
        foreach(Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}
