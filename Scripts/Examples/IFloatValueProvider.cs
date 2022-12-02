using System;
using UnityEngine;


namespace Examples
{
    public interface IFloatValueProvider
    {
        float GetValue();
    }


    [Serializable]
    public class ClampedFloatValueSource : IFloatValueProvider
    {
        [SerializeReference] private IFloatValueProvider source;
        [SerializeField] private float  lower,upper;
        
        public float GetValue()
        {
           return Mathf.Clamp(source.GetValue(), lower, upper);
        }

       
    }

    [Serializable]
    public class AngleFloatValueSource : IFloatValueProvider
    {
        [SerializeReference]
        IVectorValueProvider source1,source2;
        
        
        public float GetValue()
        {
            return Vector2.SignedAngle(source1.GetValue(),source2.GetValue());
        }
    }
    
    
    [Serializable]
    public class MagnitudeFloatValueSource : IFloatValueProvider
    {
        [SerializeReference]
        IVectorValueProvider source;
        public float GetValue()
        {
            return source.GetValue().magnitude;
        }
    }
    
}