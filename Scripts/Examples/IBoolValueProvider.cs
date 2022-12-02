using System;
using UnityEngine;


namespace Examples
{
    public interface IBoolValueProvider
    {
        bool GetValue();
    }
    
    [Serializable]
    public class ObjectActiveBoolValueProvider : IBoolValueProvider
    {
        [SerializeField] private GameObject gameObject;
        public bool GetValue()
        {
            return gameObject.activeSelf;
        }
    }
    
    [Serializable]
    public class DistanceBoolValueProvider : IBoolValueProvider
    {
        [SerializeField] private GameObject gameObject1,gameObject2;
        [SerializeField] private float distance;
        public bool GetValue()
        {
            return Vector3.Distance(gameObject1.transform.position, gameObject2.transform.position) < distance;
        }
    }

  
}