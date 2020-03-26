using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class SpriteRandomizer : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();
    public bool isRandom = true;
    public int spriteIndex;

    void Start()
    {
        UpdateSprite();
    }

    void UpdateSprite()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[spriteIndex];
        if (isRandom)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
        }
    }
}
