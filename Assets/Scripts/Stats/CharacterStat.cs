using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class CharacterStat
{
    public float BaseValue;
    public float Value
    {
        get
        {
            if (isDirty || BaseValue != lastBaseValue)
            {
                lastBaseValue = BaseValue;
                _value = calculateFinalValue();
                isDirty = false;
            }
            return calculateFinalValue();
        }
    }
    protected bool isDirty = true;
    protected float _value;
    protected float lastBaseValue = float.MinValue;


    protected readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    public CharacterStat()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }
    public CharacterStat(float baseValue) : this()
    {
        BaseValue = baseValue;
    }

    public virtual void AddModifier(StatModifier modifier)
    {
        isDirty = true;
        statModifiers.Add(modifier);
        statModifiers.Sort(CompareModifiersOrder);
    }
    protected virtual int CompareModifiersOrder(StatModifier a, StatModifier b)
    {
        if (a.order < b.order)
            return -1;
        else if (a.order > b.order) return 1;
        else return 0;
    }
    public virtual bool RemoveModifier(StatModifier modifier)
    {
        if (statModifiers.Remove(modifier))
        {
            isDirty = true;
            return true;
        };
        return false;
    }
    public virtual bool RemoveModifierFromSource(object source)
    {
        bool didRemove = false;
        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }
    protected virtual float calculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];
            if (mod.type == StatModType.Flat)
            {
                finalValue += statModifiers[i].value;
            }
            else if (mod.type == StatModType.PercentMult)
            {
                sumPercentAdd += mod.value;
                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if (mod.type == StatModType.PercentAdd)
            {
                finalValue *= 1 + mod.value;
            }

        }
        return (float)Mathf.Round(finalValue);
    }
}
