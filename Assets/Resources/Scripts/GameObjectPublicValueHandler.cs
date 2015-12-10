﻿using UnityEngine;
using System.Collections;

public class GameObjectPublicValueHandler : MonoBehaviour
{
    public bool canBeHit = false;
    public int health = 1;
    public int scoreValue = 1;
    public bool invincible = false;

    [HideInInspector]
    public GameObject gameController;
    private SpriteRenderer sr;
    [HideInInspector]
    public PlayerController pc;

    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        sr = GetComponentInChildren<SpriteRenderer>();
        pc = gameController.GetComponent<PlayerController>();
    }

    public void CheckHealth()
    {
        if (!invincible)
        {
            health--;

            if (health <= 0)
            {
                if (GetComponent<DeathExplosionEffect>())
                {
                    GetComponent<DeathExplosionEffect>().Effect();
                    if (tag == "Enemy")
                    {
                        pc.AddScore(scoreValue);
                    }
                }

                if (tag == "Player")
                {
                    pc.noDeathBonus = false;

                    pc.lives--;

                    if (pc.lives > 0)
                    {
                        gameController.GetComponent<PlayerDeath>().PlayerRespawn();
                    }
                    else
                    {
                        gameController.GetComponent<PlayerDeath>().EndGameStart(false);
                    }
                }
                else if (tag == "Enemy")
                {
                    pc.AddMultiplier();
                }
                /*if (GetComponent<AiBoss1>())
                {
                    gameController.GetComponent<PlayerDeath>().EndGameStart(true);
                }*/

                gameObject.SetActive(false);
            }
            else
            {
                if (GetComponent<HitEffect>())
                {
                    StartCoroutine(GetComponent<HitEffect>().Effect());

                }
            }
        }
    }

    public void StartInvincibility()
    {
        StartCoroutine(InvincibilityTimer());
    }

    IEnumerator InvincibilityTimer()
    {
        invincible = true;
        for (int i = 0; i < 10; i++ )
        {
            yield return new WaitForSeconds(0.1f);
            sr.color = new Color(255f, 255f, 255f, 0f);
            yield return new WaitForSeconds(0.1f);
            sr.color = new Color(255f, 255f, 255f, 255f);
        }
        invincible = false;
    }
}
