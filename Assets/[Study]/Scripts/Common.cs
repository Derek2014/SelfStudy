using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace STD{
	public class Common{
		public static T CreateObject<T>(string name)where T: MonoBehaviour{
			var gameObject = new GameObject(name);
			//			gameObject.hideFlags = HideFlags.NotEditable;
			gameObject.transform.localPosition = new Vector3(10000f, 20000f, 30000f);
			gameObject.AddComponent<T>();
			return gameObject.GetComponent<T>() as T;
		}
	}
}
