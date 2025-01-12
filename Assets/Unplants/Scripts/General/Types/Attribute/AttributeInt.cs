using System;
using System.Collections.Generic;
using Unity.Collections;

namespace Unplants.Scripts.General.Types.Attribute
{
    public class AttributeInt
    {
        private int _initialValue;
        private int _cachedValue;

        public int Value
        {
            get
            {
                if (_isDirty)
                {
                    int addictivesSum = 1;
                    int multiplicativeProduct = 1;

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

        private List<AttributeIntModifier> _modifiers;
        private bool _isDirty;

        private AttributeInt(){}

        public AttributeInt(int initialValue)
        {
            _initialValue = initialValue;
        }

        public void AddModifier(AttributeIntModifier modifier)
        {
            _modifiers.Add(modifier);
            _isDirty = true;
        }
        
        public void RemoveModifier(AttributeIntModifier modifier)
        {
            _modifiers.RemoveSwapBack(modifier);
            _isDirty = true;
        }
        
        public static explicit operator int(AttributeInt attribute)
        {
            return attribute.Value;
        }
    }
}