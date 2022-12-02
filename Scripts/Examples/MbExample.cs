using System;
using UnityEngine;


namespace Examples
{

	public class MbExample: MonoBehaviour
	{
		[SerializeReference] private IFloatValueProvider source1;
		[SerializeReference] private IVectorValueProvider source2;
		[SerializeReference] private IBoolValueProvider source3;
		
		private void Update()
		{
			Debug.Log("value from float source: "+source1.GetValue());
			Debug.Log("value from vector source: "+source2.GetValue());
			Debug.Log("value from bool source: "+source3.GetValue());
		}
	}
}