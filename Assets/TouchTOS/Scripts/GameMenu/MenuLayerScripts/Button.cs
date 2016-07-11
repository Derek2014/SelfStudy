using UnityEngine;
using System;
using System.Collections;

public class Button : iMonoBehaviour {
	[HideInInspector]
	public int _tk2dNormalSpriteId = -1;
	public int _tk2dHoverSpriteId = -1;
	
	public bool dynamicSelect = false;
	
	public bool enable = true;
	public bool enableHover = true;
	public bool enableLongTap = true;
	
	public bool passThrough = false;
	
	public bool _hover = false;
	
	public bool enableSwipe = false;
	
	public bool important = false;
	
	//Events with return current touch position
	public Action<Vector2>	onFingerDown;
	public Action<Vector2>	onFingerUp;
	public Action<Vector2>	onFingerMove;
	public Action<FingerGestures.SwipeDirection, float> onSwipe;
	public Action onLeave;
	//Events without return position
	public Action onTap;
	public Action onLongTap;
	
	public void Swipe(FingerGestures.SwipeDirection direction, float velocity){
		if(!enableSwipe || !enable || onSwipe == null) return;
		onSwipe(direction, velocity);
	}
	
	public void FingerDown(Vector2 fingerPos){
//		Watchdog.TouchLog ("[Button] ::" + this.name + ":: FingerDown recieved call");
		if(!_hover && enableHover) Hover();
		
		if(onFingerDown != null && enable){
			//enable = false;
			//Invoke("Renable", 0.05f);
//			Watchdog.TouchLog ("[Button] ::" + this.name + ":: is enabled, call action: onFingerDown()");
			onFingerDown(fingerPos);	
		}
	}
	
	public void FingerUp(Vector2 fingerPos){
		if(_hover && enableHover) Leave();
		if(onFingerUp != null && enable){
			//enable = false;
			//Invoke("Renable", 0.05f);
			onFingerUp(fingerPos);
		}
	}
	
	public void FingerMove(Vector2 fingerPos){
		if(!enable) return;
		if(onFingerMove != null) onFingerMove(fingerPos);
	}

	public void Tap(){
//		Watchdog.TouchLog ("[Button] ::" + this.name + ":: Tap recieved call");
		if(onTap != null && enable){
			//enable = false;
			//Invoke("Renable", 0.05f);
//			Watchdog.TouchLog ("[Button] ::" + this.name + ":: is enabled, call action: onTap()");
			this.onTap();
		}
	}
	
	public void LongTap(){
//		Watchdog.TouchLog ("[Button] ::" + this.name + ":: LongTap recieved call");
		if(onLongTap != null && enable && enableLongTap){
//			Watchdog.TouchLog ("[Button] ::" + this.name + ":: is enabled, call action: onLongTap()");
			onLongTap();	
		}
	}
	
	public void CleanEvents(){
		CleanFingerDown();
		CleanFingerUp();
		CleanFingerMove();
		CleanTap();
		CleanLongTap();
	}
	
	public void CleanFingerDown(){
		onFingerDown = null;
	}
	public void CleanFingerUp(){
		onFingerUp = null;
	}
	public void CleanFingerMove(){
		onFingerMove = null;
	}
	public void CleanTap(){
		onTap = null;
	}
	public void CleanLongTap(){
		onLongTap = null;
	}
	
	private tk2dSprite _sprite;
	public virtual void Hover(){
		if(gameObject == null) return;
		if(_tk2dHoverSpriteId > -1){
			if(_sprite == null) _sprite = gameObject.GetComponent<tk2dSprite>() as tk2dSprite;
			if(_sprite != null){
				if(_tk2dNormalSpriteId == -1){
					_tk2dNormalSpriteId = _sprite.spriteId;
				}
				_sprite.spriteId = _tk2dHoverSpriteId;
			}
		}
		
		_hover = true;
		iTouch.onGlobalFingerUp += _GlobaleFingerUp;
	}
	public virtual void Leave(){
		iTouch.onGlobalFingerUp -= _GlobaleFingerUp;
		if(gameObject == null) return;
		if(onLeave != null) onLeave();
		if(_tk2dNormalSpriteId > -1){
			if(_sprite == null) _sprite = gameObject.GetComponent<tk2dSprite>() as tk2dSprite;
			if(_sprite != null){
				_sprite.spriteId = _tk2dNormalSpriteId;
			}
		}
		
		_hover = false;
		
	}

	private void OnDestroy(){
		iTouch.onGlobalFingerUp -= _GlobaleFingerUp;
	}
	
	
	protected void _GlobaleFingerUp(){
		if(_hover) Leave();
	}
//	private int _ups = 1;
//	private int _updateCursor = 0;
//	
//    void Update() {
//		int cursor = (int)(Time.timeSinceLevelLoad * _ups) % 2;
//		if(! ((Time.timeScale > 0) && ( _updateCursor != cursor )))
//			return;
//		_updateCursor = cursor;
//		
//		if(gameObject.collider != null){
//	        if(GeometryUtility.TestPlanesAABB(MenuLayer.getCameraPlanes(), gameObject.collider.bounds)){
//				collider.enabled = true;
//			}
//	        else{
//				collider.enabled = false;
//			}
//		}
//		
//    }
}
