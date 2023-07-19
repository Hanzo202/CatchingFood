using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Name", menuName = "Food")]
public class FoodDate : ScriptableObject
{
    public string foodName;
    public Sprite foodSprite;
    public GameObject foodPrefab;
    public int Count;
}
