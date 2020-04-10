using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public string id = "ITEM_ID";
    public string title = "Item Name";
    public Sprite icon;
    public Sprite worldSprite;
}
