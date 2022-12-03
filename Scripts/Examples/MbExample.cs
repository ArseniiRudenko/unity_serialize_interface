using UnityEngine;
using Attributes;

namespace Examples
{

	public class MbExample: MonoBehaviour
	{
		[SerializeReference,SelectableImpl] private IFloatValueProvider source1;
		[SerializeReference,SelectableImpl] private IVectorValueProvider source2;
		[SerializeReference,SelectableImpl] private IBoolValueProvider source3;
		
		private void Update()
		{
			Debug.Log("value from float source: "+source1.GetValue());
			Debug.Log("value from vector source: "+source2.GetValue());
			Debug.Log("value from bool source: "+source3.GetValue());
		}
	}
}