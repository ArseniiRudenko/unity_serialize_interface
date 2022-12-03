using System;
using UnityEngine;

namespace Examples
{
    
    public interface IVectorValueProvider
    {
        Vector2 GetValue();
    }
   
 
    
    [Serializable]
    public class TransformForwardVectorValueProvider : IVectorValueProvider
    {
        [SerializeField] Transform transform;
        [SerializeField] private Transform projectedOn;

        public Vector2 GetValue()
        {
            Vector3 v3;
            if(projectedOn == null)
                v3 = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
            else
            {
                var vector = transform.forward;
                var prj = Vector3.ProjectOnPlane(vector, projectedOn.up);
                v3 = projectedOn.InverseTransformVector(prj);
            }
            return new Vector2(v3.x, v3.z);
        }
    }
    
    
    [Serializable]
    public class TransformDifferenceVectorValueProvider : IVectorValueProvider
    {
        [SerializeField] private Transform endPoint;
        [SerializeField] public Transform startPoint;
        [SerializeField] private Transform projectedOn;


        public Vector2 GetValue()
        {
            var vector = (endPoint.position - startPoint.position).normalized;
            if(projectedOn == null)
                return Vector3.ProjectOnPlane(vector, Vector3.up);
            var prj = Vector3.ProjectOnPlane(vector, projectedOn.up);
            return projectedOn.InverseTransformVector(prj);
        }
        
       
    }
    
}