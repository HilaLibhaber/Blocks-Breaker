using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // configuration parameters
    [SerializeField] AudioClip collisionSound;
    [SerializeField] GameObject blockSpsrklesVFX;
    //[SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    // cached reference
    Level level;

    // state variables
    int hitsCount;


    private void Start()
    {
        CountBreakablieBlocks();
    }

    private void CountBreakablieBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHits();
        }
    }

    private void HandleHits()
    {
        hitsCount++;
        int maxHits = hitSprites.Length + 1;
        if (hitsCount >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void DestroyBlock()
    {
            PlayBlockDestroySFX();
            Destroy(gameObject);
            level.BlockDestroyed();
            TriggerSparkesVFX();
    }
    private void ShowNextHitSprite()
    {
        int spriteIndex = hitsCount - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
            UpdateScoreOnHit();
        }
    }

    private static void UpdateScoreOnHit()
    {
        FindObjectOfType<GameSession>().UpdateScore();
    }

    private void PlayBlockDestroySFX()
    {
        AudioSource.PlayClipAtPoint(collisionSound, Camera.main.transform.position);
        UpdateScoreOnHit();
    }

    private void TriggerSparkesVFX()
    {
        GameObject sparkles = Instantiate(blockSpsrklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
