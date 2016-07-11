using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public partial class iTouch {
	
	public static List<TouchRecord> touchRecords;
	
	public static void CaptureTouchEvent(TouchRecord.TouchType touchType, int fingerIndex, Vector2 position){
//		if(_instance == null) return;
//		
//		Vector2 screenPosition = SF.getRelativeVector(position);
//		if(touchRecords == null) touchRecords = new List<TouchRecord>();
//		touchRecords.Add(new TouchRecord(touchType, fingerIndex, position));
	}
	
	public class TouchRecord{
		public enum TouchType{
			DOWN = 1,
			UP = 2,
			MOVE = 3
		}
		
		public TouchType touchType;
		public int fingerIndex;
		public Vector2 position;
		public float time;
		
		public TouchRecord(TouchType touchType, int fingerIndex, Vector2 position){
			this.fingerIndex = fingerIndex;
			this.touchType = touchType;
			this.position = position;
			this.time = Time.time;
		}
	}
	
	public int touchCount = 0;
	
	private int updatePerSecond = 1;
	private int updateCursor = 0;
	public void Update(){
		int cursor = (int)(Time.timeSinceLevelLoad * updatePerSecond) % 2;
		
		if((Time.timeScale > 0) && ( updateCursor != cursor )){
			if(touchRecords != null)
				touchCount = touchRecords.Count;
		}
	}
}
