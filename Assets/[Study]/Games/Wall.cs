using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	public Transform container_T;

	public void SetAngle(float angle){

		container_T.localEulerAngles = new Vector3(0, 0, angle);
	}

}
