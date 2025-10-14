using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> sprites;
    private int spriteCount;
    private int currentIndex;
    private SpriteRenderer sr;

    private void Awake()
    {
        currentIndex = 0;
        spriteCount = sprites != null ? sprites.Count : 0;
        sr = GetComponent<SpriteRenderer>();
    }

    public int GetIndex()
    {
        return currentIndex;
    }

    public void SetSprite(int index)
    {
        if (index < spriteCount) 
        { 
            sr.sprite = sprites[index];
            currentIndex = index;
        }
    }

    public void NextSprite()
    {
        currentIndex = (currentIndex + 1) % spriteCount;
        sr.sprite = sprites[currentIndex];
    }
}
