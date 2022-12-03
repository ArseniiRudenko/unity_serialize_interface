using System;
using UnityEngine;
using Attributes;


namespace Examples
{
    public interface IFloatValueProvider
    {
        float GetValue();
    }


    [Serializable]
    public class ClampedFloatValueSource : IFloatValueProvider
    {
        [SerializeReference,SelectableImpl] private IFloatValueProvider source;
        [SerializeField] private float  lower,upper;
        
        public float GetValue()
        {
           return Mathf.Clamp(source.GetValue(), lower, upper);
        }

       
    }

    [Serializable]
    public class AngleFloatValueSource : IFloatValueProvider
    {
        [SerializeReference,SelectableImpl]
        IVectorValueProvider source1,source2;
        
        
        public float GetValue()
        {
            return Vector2.SignedAngle(source1.GetValue(),source2.GetValue());
        }
    }
    
    
    [Serializable]
    public class MagnitudeFloatValueSource : IFloatValueProvider
    {
        [SerializeReference,SelectableImpl]
        IVectorValueProvider source;
        public float GetValue()
        {
            return source.GetValue().magnitude;
        }
    }
    
}