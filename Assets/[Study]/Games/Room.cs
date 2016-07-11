using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

	public PositionType positionType;
	public enum PositionType { //position related to the Room being extended from
		ORIGIN = 0,
		VL,
		VR,
		HT,
		HB
	};


	public void Setup(PositionType positionType){
		this.positionType = positionType;
		CreateRoom(10);

	}


	void CreateRoom(int numOfBox){



	}
}
