using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int hitsRemaining = 5;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer innerSpriteRenderer;
    private TextMeshPro text;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        innerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshPro>();
        UpdateBlock();
    }

    private void UpdateBlock()
    {
        text.SetText(hitsRemaining.ToString());
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, hitsRemaining / 15f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        innerSpriteRenderer.color = Color.white;
        Debug.Log(innerSpriteRenderer.color);
        hitsRemaining--;
        if (hitsRemaining > 0) UpdateBlock();
        else Destroy(gameObject);
        
        //GetComponentInChildren<SpriteRenderer>().color = Color.black;
    }

    internal void SetHits(int hits)
    {
        hitsRemaining = hits;
        UpdateBlock();
    }
}
