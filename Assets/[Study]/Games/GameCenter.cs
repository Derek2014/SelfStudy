using UnityEngine;
using System.Collections;

public class GameCenter : iMonoBehaviour {
	public Transform [] teleports;

	//public GameObject character;

	public CharacterController character;

	public void Awake(){
		//Debug.Log ("Awake");
		Stage01();

		Debug.Log ("Scr Width: " + Screen.width + " | Scr Height: " + Screen.height);
	}

	public void OnEnable(){		
		//Debug.Log ("OnEnable()");
		iTouch.onGlobalFingerDown	+= _OnFingerDown;
		iTouch.onGlobalFingerUp		+= _OnFingerUp;
		iTouch.onGlobalFingerMove	+= _OnFingerMove;
	}
	public void OnDisable(){
		//Debug.Log ("OnDisable()");
		iTouch.onGlobalFingerDown	-= _OnFingerDown;
		iTouch.onGlobalFingerUp		-= _OnFingerUp;
		iTouch.onGlobalFingerMove	-= _OnFingerMove;
	}

	Vector2 addForce = Vector2.zero;
	float dragDistance = 0;
	float dragAngle = 0;
	Vector2 downPos = Vector2.zero;
	Vector2 movingPos = Vector2.zero;
	private void _OnFingerDown(Vector2 fingerPos){
		//Debug.Log ("_OnFingerDown pos: " + fingerPos);
		downPos = fingerPos;
	}
	private void _OnFingerUp(){
		//Debug.Log ("_OnFingerUp");
		dragDistance = Vector2.Distance(downPos, movingPos);
		Vector2.Dot(downPos, movingPos);
		dragAngle = Vector2.Angle(downPos, movingPos);

		addForce.x = (downPos.x - movingPos.x)*forceMultiplier;
		addForce.y = (downPos.y - movingPos.y)*forceMultiplier;

		Debug.Log ("Angle: " + dragAngle);

		//Debug.Log ("Dot: " + Vector2.Dot(downPos, movingPos) + 
		//           "\n Angle: End=" + dragAngle + " | Begin=" + Vector2.Angle(movingPos, downPos));
		//Fire(downPosToBegin, dragDistance);

		if(character.canJump){
			character.canJump = false;
			Fire (addForce);
		}
	}
	private void _OnFingerMove(Vector2 fingerPos){
		//Debug.Log ("_OnFingerMove pos: " + fingerPos);
		movingPos = fingerPos;
	}

	
	public void Fire(Vector2 force){
		Debug.Log ("Fire() force: " + force);
		character.GetComponent<Rigidbody2D>().AddForce(force);
	}


	public void CreateRoom(){


	}


	public void Stage01(){
		Box box = null;
		for(int y = 0; y < 10; y++){
			for(int x = y; x < 10; x++){
				box = ResourcesManager.PrefabLoader.Load<Box>(ResourcesManager.PrefabLoader.ID.Box_Prefab);
				box.transform.parent = this.transform;
				box.GetComponent<Rigidbody2D>().Sleep();
				box.transform.localPosition = new Vector3(5+ 0.16f*x, 0.16f*y, 0);
			}
		}

		Wall wall = null;
		for(int i = 0; i < 40; i++){
			wall = ResourcesManager.PrefabLoader.Load<Wall>(ResourcesManager.PrefabLoader.ID.Wallx7_Hon_Prefab);
			wall.transform.parent = this.transform;
			float x = (i % 2 == 0)? 1f:0f;
			wall.transform.localPosition = new Vector3(x, UnityEngine.Random.Range(0f, 0.2f) + 0.6f*i, 0);
			wall.SetAngle(UnityEngine.Random.Range(0, 10)-5);
		}

	}


	float forceMultiplier = 1f;
	public void SuperMode(){
		character.GetComponent<Rigidbody2D>().mass = 100;
		character.transform.localScale = Vector3.one*5;
		forceMultiplier = 100;
	}
	
	public void NormalMode(){
		character.GetComponent<Rigidbody2D>().mass = 1;
		character.transform.localScale = Vector3.one;
		forceMultiplier = 1;
	}


}
