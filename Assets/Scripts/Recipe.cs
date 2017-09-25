using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class Recipe
{
    public string Name;
    public LiquidType Liquid;
    public List<RequiredIngredientDesc> Ingredients;

    public GameObject ProjectilePrefab;
    public Sprite Icon;
    public float Damage = 2;
}