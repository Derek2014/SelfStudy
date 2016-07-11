using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public Transform [] teleports;
	public bool canJump = true;

	void OnTriggerEnter2D(Collider2D col){
//		Debug.Log ("OnTriggerEnter..." + col.gameObject.name + " | " + col.collider2D.gameObject.name);

		if(col.transform == teleports[1]){
			Vector3 pos = teleports[0].transform.position;
			this.transform.position = new Vector3(pos.x, pos.y, this.transform.position.z);
		}

	}

	void OnCollisionEnter2D(Collision2D col){
		Debug.Log (col.gameObject.name + "<- name | layer ->" +col.gameObject.layer.ToString());

		canJump = true;
		bool isTelePort = false;

		for(int i = 0; i < teleports.Length; i++){
			if(col.transform == teleports[i]){
				isTelePort = true;
			}
		}

//		Debug.Log ("isTelePort: " + isTelePort);
		if(!isTelePort){
//			float f = this.rigidbody2D.angularVelocity;
//			Debug.Log ("force: " + f);
//			this.rigidbody2D.AddForce(new Vector2(((f > 0)? 0:0), 100));

			//this.hingeJoint.rigidbody.AddRelativeForce(
		}
		//this.rigidbody2D.
//		Debug.Log ("OnCollisionEnter..." + col.gameObject.name);
	}

}
