using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pallets;
    
    public int ghostMultiplier { get; private set; }
    public int score { get; private set;  }
    public int lives { get; private set;  }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (this.lives <= 0 && Input.anyKeyDown)
            {
                NewGame();
            }
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
        foreach (Transform pallet in this.pallets )
        {
            pallet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void ResetState()
    {
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
            
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
            
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + ghost.points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);
        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            ResetState();
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }

    public void PalletEaten(Pallet pallet)
    {
        pallet.gameObject.SetActive(false);
        SetScore(this.score + pallet.points);

        if (!HasRemainingPallets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void BigPalletEaten(BigPallet pallet)
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pallet.duration);
        }
        
        PalletEaten(pallet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pallet.duration);
    }

    private bool HasRemainingPallets()
    {
        foreach (Transform pallet in this.pallets)
        {
            if (pallet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }
}

