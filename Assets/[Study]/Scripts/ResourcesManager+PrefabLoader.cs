using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace ResourcesManager {
	public class PrefabLoader : iMonoBehaviour{
		private static PrefabLoader _instance;
		private static PrefabLoader instance{
			get{
				if(_instance == null){
					_instance = STD.Common.CreateObject<PrefabLoader>("[ResourcesManager.PrefabLoader]");
				}
				return _instance;
			}
		}
	
		public enum ID{
			Wallx7_Hon_Prefab,
			Wallx7_Ver_Prefab,
			Box_Prefab,


			SimpleButton,

		}


		public static T Load<T>(ID id) where T: MonoBehaviour {
			return Load<T>(id, null, Vector3.zero);
		}

		public static T Load<T>(ID id, Transform t) where T: MonoBehaviour {
			return Load<T>(id, t, Vector3.zero);
		}

		public static T Load<T>(ID id, Vector3 position) where T: MonoBehaviour {
			return Load<T>(id, null, position);
		}

		public static T Load<T>(ID id, Transform t, Vector3 position) where T: MonoBehaviour {
			GameObject resourceObjecet = Resources.Load("Prefabs/" + id.ToString()) as GameObject ;
			if(resourceObjecet != null)
			{
				GameObject go = GameObject.Instantiate(resourceObjecet) as GameObject;
				if(go != null)
				{
//					Debug.Log("resources object is not null!") ;
					go.name = "[RM]" + id.ToString();
					if(t == null) t = instance.transform;	
					go.transform.parent = t.transform;
					go.transform.localScale = Vector3.one;
					go.transform.localPosition = position;
					return go.GetComponent<T>();
				}
				else
				{					
					Debug.Log("resources object is null!") ;
					return null ;
				}
			}
			else
				return null ;
		}

	}
}
