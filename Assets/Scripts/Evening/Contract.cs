using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contract : MonoBehaviour
{
    [Header("Buffs")]
    public int ID;

    [Header("Debuffs")]
    public Vector2Int hp_nerf;
    public Vector2 hp_regen_nerf;
    public Vector2Int stamina_nerf;
    public Vector2 stamina_regen_nerf;
    public Vector2Int walk_speed_nerf;
    public Vector2Int run_speed_nerf;

    [Header("Purchases")]
    public int maxPurchases;
}