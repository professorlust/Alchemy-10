﻿using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Effect", menuName = "Alchemy/Effect")]
public class Effect : ScriptableObject
{
    public new string name = "New Effect";
    public string description = "Effect description";

    public float minConc;
    public float maxConc;
    public float bestConc;

    public List<PhaseMultiplier> phaseMultiplier = new List<PhaseMultiplier>();

    private int tier = 1;

    public float GetPotency(float concentration)
    {
        if(concentration < minConc)
        {
            return 0f;
        }
        if (concentration == bestConc)
        {
            return 1f;
        }
        if(concentration > maxConc)
        {
            return 0f;
        }

        if(minConc <= concentration && concentration < bestConc)
        {
            float pot = (concentration - minConc);
            pot /= (bestConc - minConc);
            pot *= Mathf.PI / 2;
            pot = Mathf.Sin(pot);
            return Mathf.Pow(pot, 2f);
        }

        if (bestConc < concentration && concentration <= maxConc)
        {
            float pot = (concentration - bestConc);
            pot /= (maxConc - bestConc);
            pot *= Mathf.PI / 2;
            pot = Mathf.Cos(pot);
            return Mathf.Pow(pot, 2f);
        }

        return 0f;
    }

    public float Tier()
    {
        return 1/(1 + Mathf.Exp(-tier));
    }

    public void IncreaseTier(int tierChange)
    {
        tier += tierChange;
    }

    public float PhaseMultiplier(IngredientType type)
    {
        float multiplier = 1f;
        PhaseMultiplier phMu = phaseMultiplier.Find(phase => phase.type == type);
        if(phMu != null)
        {
            multiplier = phMu.multiplier;
        }
        return multiplier;
    }

    public void Apply(CharaController target)
    {
    }
}
