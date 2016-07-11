using UnityEngine;
using System.Collections;

//Designed by Apple in California Assembled in China
public class iMonoBehaviour : MonoBehaviour {

	protected virtual void Awake()
	{
	}

	private Transform _cachedTransform;
	public new Transform transform
	{
		get
		{
			if( _cachedTransform == null )
			{
//				Debug.LogError("Cache Transform: " + this.name);
				_cachedTransform = GetComponent<Transform>();
			}
//			else {
////				Debug.LogError("Get Cache Transform: " + this.name);
//			}
			return _cachedTransform;
		}
	}
	
	private Renderer _cachedRenderer;
	public new Renderer renderer
	{
		get
		{
			if( _cachedRenderer == null )
			{
				_cachedRenderer = GetComponent<Renderer>();
			}
			return _cachedRenderer;
		}
	}
	
	public Vector3 GetLocalPositionOf(Transform targetTransform){
		if(targetTransform == null){
			return transform.position;
		}
		float x = transform.position.x - targetTransform.position.x;
		float y = transform.position.y - targetTransform.position.y;
		float z = transform.position.z - targetTransform.position.z;
		return new Vector3(x, y, z);
	}
	
	public void SetActive(bool isActive){
		gameObject.SetActive(isActive);
	}
}
