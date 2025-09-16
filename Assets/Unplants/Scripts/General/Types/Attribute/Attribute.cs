using System;
using System.Collections.Generic;
using Unity.Collections;

namespace Unplants.Scripts.General.Types.Attribute
{
    public class Attribute
    {
        private float _initialValue;
        private float _cachedValue;

        public float Value
        {
            get
            {
                if (_isDirty)
                {
                    float addictivesSum = 1;
                    float multiplicativeProduct = 1;

                    foreach (var mod in _modifiers)
                    {
                        switch (mod.ModType)
                        {
                            case EModType.Multiplicative:
                                multiplicativeProduct *= 1 + mod.Value;
                                break;
                            case EModType.Addictive:
                                addictivesSum += mod.Value;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }

                    _initialValue *= addictivesSum * multiplicativeProduct;
                    _isDirty = false;
                }

                return _cachedValue;
            }
        }

        private List<AttributeModifier> _modifiers;
        private bool _isDirty;

        private Attribute(){}

        public Attribute(float initialValue)
        {
            _initialValue = initialValue;
        }

        public void AddModifier(AttributeModifier modifier)
        {
            _modifiers.Add(modifier);
            _isDirty = true;
        }
        
        public void RemoveModifier(AttributeModifier modifier)
        {
            _modifiers.RemoveSwapBack(modifier);
            _isDirty = true;
        }
        
        public static explicit operator float(Attribute attribute)
        {
            return attribute.Value;
        }
    }
}