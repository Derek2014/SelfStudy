using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public partial class iTouch : MonoBehaviour {
	private static iTouch _instance;
	
	public static Action onGlobalFingerUp;
	public static Action<Vector2> onGlobalFingerDown;
	public static Action<Vector2> onGlobalFingerMove;
	
	private List<Button> _pickedButtons		= new List<Button>();
	private List<Button> _lastPickedButtons = new List<Button>();
	private List<Button> _fingerDownButtons = new List<Button>();
	
	public static void ClearFingerDown(){
		_instance._fingerDownButtons.Clear();
	}
	
	public static bool enable {
		get{
			if(_instance == null) return false;
			return _instance._enable;
		}
		set{
			if(_instance != null)
				_instance._enable = value;
		}
	}
	private bool _enable = true;
	
	public static bool debugConsoleFlag = true;
	
	void Awake(){
		_instance = this;
	}

	void OnEnable(){
		FingerGestures.OnFingerDown	+= FG_OnFingerDown;			
        FingerGestures.OnFingerMove	+= FG_OnFingerMove;
		FingerGestures.OnFingerUp	+= FG_OnFingerUp;
		FingerGestures.OnLongPress  += FG_OnFingerLongTap;
		FingerGestures.OnSwipe		+= FG_OnSwipe;
	}
	void OnDisable(){
		FingerGestures.OnFingerDown -= FG_OnFingerDown;
        FingerGestures.OnFingerMove -= FG_OnFingerMove;
		FingerGestures.OnFingerUp	-= FG_OnFingerUp;
		FingerGestures.OnLongPress  -= FG_OnFingerLongTap;
		FingerGestures.OnSwipe		-= FG_OnSwipe;
	}
		
	private void FG_OnSwipe(Vector2 startPos, FingerGestures.SwipeDirection direction, float velocity){
#if !DEVELOPMENT
		try{
#endif
			TouchEventHandler((button) => {
				button.Swipe(direction, velocity);
			}, startPos);
#if !DEVELOPMENT
		}
		catch(Exception e){
			if(!sentExceptionA) ThrowException(e);
			sentExceptionA = true;
		}
#endif
	}

	public static bool sentExceptionA = false;
	public static bool importantTouch = false;
	private void FG_OnFingerDown( int fingerIndex, Vector2 fingerPos )
    {		
		ClearFingerDown();
		importantTouch = false;
#if !DEVELOPMENT
		try{
#endif
			if(onGlobalFingerDown != null) onGlobalFingerDown(fingerPos);
			if(!IsAcceptableFinger(fingerIndex)) return;
			importantTouch = TouchEventHandler((button) => {
				button.FingerDown(fingerPos);
				_fingerDownButtons.Add(button);
			}, fingerPos);
#if !DEVELOPMENT
		}
		catch(Exception e){
			if(!sentExceptionA) ThrowException(e);
			sentExceptionA = true;
		}
#endif
    }
	
	public static bool sentExceptionB = false;
    private void FG_OnFingerUp( int fingerIndex, Vector2 fingerPos, float timeHeldDown )
    {
#if !DEVELOPMENT
		try{
#endif
			if(onGlobalFingerUp != null) onGlobalFingerUp();
			if(!IsAcceptableFinger(fingerIndex)) return;
			TouchEventHandler((button) => {
				button.FingerUp(fingerPos);
				if(_fingerDownButtons.Contains(button)){
					button.Tap();
				}
			}, fingerPos);
#if !DEVELOPMENT
		}
		catch(Exception e){
			if(!sentExceptionB) ThrowException(e);
			sentExceptionB = true;
		}
#endif
    }
	
	public static bool sentExceptionC = false;
    private void FG_OnFingerMove( int fingerIndex, Vector2 fingerPos )
    {
#if !DEVELOPMENT
		try{
#endif
			if(!IsAcceptableFinger(fingerIndex)) return;
			if(onGlobalFingerMove != null) onGlobalFingerMove(fingerPos);
			
			TouchEventHandler((button)=>{
				if(button != null){
					button.FingerMove(fingerPos);
				}
			}, fingerPos);
			
			if(_lastPickedButtons != null){
				List<Button> expectedButtons = (_pickedButtons == null)? _lastPickedButtons : (_lastPickedButtons.Except(_pickedButtons)).ToList();
				foreach(Button expectedButton in expectedButtons){
					if(expectedButton != null){
						if(expectedButton.enableHover)
							expectedButton.Leave();
					}
				}
			}
			if(_pickedButtons != null){
				List<Button> newButtons = (_lastPickedButtons == null) ? _pickedButtons : (_pickedButtons.Except(_lastPickedButtons)).ToList();
				foreach(Button newButton in newButtons){
					if(newButton != null){
						if(newButton.enableHover)
							if(newButton.dynamicSelect || _fingerDownButtons.Contains(newButton))
								newButton.Hover();
					}
				}
			}
			_lastPickedButtons = _pickedButtons;
#if !DEVELOPMENT
		}
		catch(Exception e){
			if(!sentExceptionC) ThrowException(e);
			sentExceptionC = true;
		}
#endif
    }
	
	public static bool sentExceptionD = false;
    private void FG_OnFingerLongTap(Vector2 fingerPos )
    {
#if !DEVELOPMENT
		try{
#endif
			TouchEventHandler((button) => {
				if(button.onLongTap != null){
					button.LongTap();
					ClearFingerDown();
				}
			}, fingerPos);
#if !DEVELOPMENT
		}
		catch(Exception e){
			if(!sentExceptionD) ThrowException(e);
			sentExceptionD = true;
		}
#endif
    }
	
	private List<Camera> GetAvailableCameras(Vector2 fingerPos){
		List<Camera> cameras = new List<Camera>();
		
		if(!_instance._enable)
			return cameras;
		
//		Camera camera = MenuLayer.getCamera();
//		if(camera != null){
//			if(SF.getRelativeVector(fingerPos, camera).y > 0f && SF.getRelativeVector(fingerPos, camera).y < 960f){
//				if(SF.getRelativeVector(fingerPos, camera).x > 0f && SF.getRelativeVector(fingerPos, camera).x < 640f){
//					if(IsInputAvailable()){
//						cameras.Add(MenuLayer.getCamera());
//					}
//				}
//			}
//		}
//		camera = ChatCamera.GetCamera();
//		if(camera != null){
//			if(SF.getRelativeVector(fingerPos, camera).y > 0f && SF.getRelativeVector(fingerPos, camera).y < 960f){
//				if(SF.getRelativeVector(fingerPos, camera).x > 0f && SF.getRelativeVector(fingerPos, camera).x < 640f){
//					if(!ChatBubbleController.isBubbleScaling)
//						cameras.Add(ChatCamera.GetCamera());
//				}
//			}
//		}
		
		return cameras;
	}
	
	public static bool isBlockedByGuildOption = false;
	public static bool IsInputAvailable(){
#if DEBUG_CONSOLE
		if(!debugConsoleFlag) return false;
#endif
//		if(WebView.isShowing()) return Restrict("Webview is overlaying");
//		if(MenuLayer.isMenuOverlaying) return Restrict("Menu overlay layer enabled");
//		if(CardOptions_View.promptBlock) return Restrict("Card option view is prompting");
//		if(Overlay_CardOptions_Refine.refineBlock) return Restrict("UI is under refine progress");
//		if(isBlockedByGuildOption) return Restrict("Guild overlay or Ranking overlay blocked");
//		if(ViewController.block && (ViewController.dialogBlock || ViewController.progressDialogBlock)) return true;
//		if(ViewController.block) return Restrict("ViewController blocked");
		return true;
	}

	private static bool Restrict(string reason){
#if DEVELOPMENT
		Watchdog.LogError("iTouch Restricted, reason: " + reason);
#endif
		return false;
	}
	
	
	
	public static bool sentExceptionF = false;
	private bool TouchEventHandler(Action<Button> action, Vector2 fingerPos){
		List<Button> buttons = new List<Button>();
		
		foreach(Camera camera in GetAvailableCameras(fingerPos)){
			if(camera != null && camera.enabled){
				buttons = buttons.Concat(PickButtons(camera, fingerPos)).ToList();
			}
		}		
		
		foreach(Button button in buttons){
			if(button.important){
				action(button);
				return true;
			}
		}
		
		bool skip = false;
		_pickedButtons = new List<Button>();
		foreach(Button button in buttons){
			if(skip == false) {
				if(button != null){
					if(button.passThrough != true)
						skip = true;

					_pickedButtons.Add(button);
					action(button);
				}

			}
		}

		return false;
	}
	
	public static bool sentExceptionG = false;
   	public static List<Button> PickButtons(Camera camera, Vector2 screenPos)
    {
#if !DEVELOPMENT
		try {
#endif
	        Ray ray = camera.ScreenPointToRay( screenPos );
	        RaycastHit[] hits  = Physics.RaycastAll( ray, Mathf.Infinity);
			
			if(hits.Length > 0){
				List<Button> buttons = new List<Button>();
				foreach(RaycastHit hit in hits){
					Button button = hit.collider.gameObject.GetComponent<Button>() as Button;
					if(button != null){
						if(button.important){
							return new List<Button>(){button};
						}
						buttons.Add(button);	
					}
				}
	
				return (from button in buttons orderby button.transform.position.z ascending select button).ToList();
			}
#if !DEVELOPMENT
		}
		catch(Exception e){
			if(!sentExceptionG) ThrowException(e);
			sentExceptionG = true;
		}
#endif
		return new List<Button>();
    }
	
	public static void ThrowException(Exception e){
		Debug.LogError(e.ToString() + "\n" + e.StackTrace);
	}
	
	
	private bool IsAcceptableFinger(int fingerIndex){
//		#if UNITY_ANDROID
//		if(fingerIndex > 0) {
//		}
//		#else
//		if(fingerIndex > 0) return false;
//		#endif
		if(fingerIndex > 0) return false;
		return true;
	}
}
