using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpecialType
{
    Default,
    Checkpoint,
    Stages,
    Collectables,
    Obstacles,
    Stationary
}
[System.Serializable]
public class ColorToPrefab
{
    public string name;
    public Color color;
    public GameObject prefab;
    public SpecialType type = SpecialType.Default;
    public Vector2 offset;
    public Vector2 size;
    [Range(0f, 2f)]
    public float scaleMultiplier = 1f;
}
