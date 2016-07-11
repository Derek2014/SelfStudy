using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;

public class Watchdog {
//	private static int _logFlushValve = 10;
	private static int MAXBUFFER = 0;
	
	public enum LogLevel {
		LOG = 0,
		WARNING = 1,
		ERROR = 2,
		EXCEPTION = 3
	}
	
	public class LogObject {
		public LogLevel level;
		public string message;
	}
	
	private static Dictionary<string, double> _timers = new Dictionary<string, double>();
	private static List<LogObject> _logs = new List<LogObject>();
//	private static List<LogObject> _touchLog = new List<LogObject>();
//	private static List<LogObject> _textLog = new List<LogObject>();
	
//	private static List<string> _whiteList = new List<string>(){
//		"*** BootLoader start ***",
//		"*** BootLoader restore flow ***",
//		"*** BootLoader normal flow ***",
//		"*** BootLoader loading completed ***",
//		"*** RespondChecker : Validate -> Deserialize<GameJSON.Config>() ***",
//		"*** RespondChecker : Validate -> Deserialize<GameJSON.Config>() End ***",
//		"*** RespondChecker : Validate -> Deserialize<GameJSON.Login>() ***",
//		"*** RespondChecker : Validate -> Deserialize<GameJSON.Login>() End ***",
//		"** Network Completed **",
//		"** Network Failed **",
//		"** Network Waiting **",
//		"** URLRequest Receive() **",
//		"*** URLRequest Delay ***",
//		"*** URLRequest Success ***",
//		"*** URLRequest Failed ***"
//	};
	
	public static void SetTimer(string name) {
		if(!_timers.ContainsKey(name))
			_timers.Add(name, GetMicroTime());
		else
			_timers[name] = GetMicroTime();
	}
	
	public static void ReadTimer(string name) {
		Log("[Timer] " + name + " : " + ReadTimer(name, true).ToString("#.##") + " ms");
	}
	
	public static double ReadTimer(string name, bool rtn) {
		if(!_timers.ContainsKey(name)) SetTimer(name);
		
		return GetMicroTime() - _timers[name];
	}
	
	public static void ClearTimer(string name) {
		_timers.Remove(name);
	}
	
	public static int GetTimeStamp() {
		return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
	}
	
	public static int GetTimeStamp(DateTime dt) {
		return (int)(DateTime.UtcNow - dt).TotalSeconds;
	}
	
	public static double GetMicroTime() {
		return (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
	}
	
	public static DateTime GetDateTime(int unixTimeStamp) {
	    return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp).ToUniversalTime();
	}
	
	public static void SetLogBuffer(int maxLines){
		MAXBUFFER = maxLines;	
	}
	
	public static void Log(object message) {
		_Log(message.ToString(), "normal");
	}
	
	public static void LogWarning(object message) {
		_Log(message.ToString(), "warning");
	}
	
	public static void LogError(object message) {
		_Log(message.ToString(), "error");
	}
	
	public static void LogException(Exception e) {
		_Log (e.ToString() + "\n" + e.Message + "\n" + e.StackTrace, "exception");	
	}
		
	private static void _Log(string message, string type = "normal") {
//		#if !PLAYER_DEBUG
//		if(!Debug.isDebugBuild) return;
//		#endif
		
//		if(message.Length >= 3) if(message.Substring(0, 3) != "***") return;
//		if(!UnityEngine.Debug.isDebugBuild && !_whiteList.Contains(message.ToString())) return;
//		bool endFlush = _whiteList.Contains(message.ToString());
//		
//		#if UNITY_EDITOR
//		UnityEngine.Debug.Log(message);
//		#endif
		
		message = String.Format("[{0:#,###0}ms] : {1}", ReadTimer("AppLaunch", true), message);
		
//		StackTrace stackTrace;
//		stackTrace = new StackTrace();
//		StackFrame frame = stackTrace.GetFrame(2);
//		if(frame != null ) message += "  ," + " @From: " + frame.GetMethod() + "";
//		frame = stackTrace.GetFrame(3);
//		if(frame != null ) message += "  ," + " @From: " + frame.GetMethod() + "";
		
		#if !PLAYER_DEBUG
		if(MAXBUFFER > 0 && _logs.Count >= MAXBUFFER) _logs.RemoveAt(0);
		#endif
		
		switch(type){
			case "normal":
//				Debug.Log(message);
				_logs.Add(new LogObject(){ level = LogLevel.LOG, message = message });
//				Debug.Log(string.Format("[{0}] {1}\n", LogLevel.LOG.ToString(), message));
				break;
			case "warning":
//				Debug.LogWarning(message);
				_logs.Add(new LogObject(){ level = LogLevel.WARNING, message = message });
//				Debug.Log(string.Format("[{0}] {1}\n", LogLevel.WARNING.ToString(), message));
				break;
			case "error":
//				Debug.LogError(message);
				_logs.Add(new LogObject(){ level = LogLevel.ERROR, message = message });
//				Debug.Log(string.Format("[{0}] {1}\n", LogLevel.ERROR.ToString(), message));
				break;
			case "exception":
				_logs.Add(new LogObject(){ level = LogLevel.EXCEPTION, message = message });
				break;
		}
		
		//if(_logs.Count >= _logFlushValve) Flush();
		//Flush();
	}
	
	public static string Flush(bool clearAfterFlush = false){
//		#if !PLAYER_DEBUG
//		if(!Debug.isDebugBuild) return "";
//		#endif
		
		if(_logs.Count == 0) return "";
		string output = "";

		_logs.Reverse();
		
//		if(MAXBUFFER > 0 && _logs.Count > MAXBUFFER) _logs.RemoveRange(MAXBUFFER, _logs.Count - MAXBUFFER);
		
		_logs.ForEach((line) => {
			string color = "white";
			switch(line.level){
				case LogLevel.LOG: color = "white"; break;
				case LogLevel.WARNING: color = "yellow"; break;
				case LogLevel.ERROR: color = "#ff6699ff"; break;
				case LogLevel.EXCEPTION: color = "#ff6699ff"; break;
			}
			output += string.Format("<color={0}>[{1}] {2}</color>\r\n", color, line.level.ToString(), line.message);
		});
		
		if(clearAfterFlush)
			_logs.Clear();
		else
			_logs.Reverse();

		return output;
	}
	
	public static void Clear(){
		_logs.Clear();
	}
	
//	public static void TouchLog(object message)
//	{
//		#if !PLAYER_DEBUG
//		if(!Debug.isDebugBuild) return;
//		#endif
//		
//		string log = message.ToString();
//		log = String.Format("[{0:#,###0}ms] : {1}", ReadTimer("AppLaunch", true), message);
//		if(_touchLog.Count >= MAXBUFFER) _touchLog.RemoveAt(0);
//		_touchLog.Add( new LogObject(){ level = LogLevel.LOG, message = log } );
//	}
//	
//	public static string FlushTouchLog(){
//		#if !PLAYER_DEBUG
//		if(!Debug.isDebugBuild) return "";
//		#endif
//		
//		if(_touchLog.Count == 0) return "";
//		string output = "";
//		
//		_touchLog.Reverse();
//		//if(_logs.Count > MAXBUFFER) _logs.RemoveRange(MAXBUFFER, _logs.Count - MAXBUFFER);
//		_touchLog.ForEach((line) => {
//			string color = "white";
//			switch(line.level){
//				case LogLevel.LOG: color = "white"; break;
//				case LogLevel.WARNING: color = "yellow"; break;
//				case LogLevel.ERROR: color = "#ff6666ff"; break;
//			}
//			output += string.Format("<color={0}>[{1}] {2}</color>\r\n", color, line.level.ToString(), line.message);
//		});
//		_touchLog.Reverse();
//		//_logs.Clear();
//
//		return output;
//	}
//	
//	public static void TextLog(object message)
//	{
//		#if !PLAYER_DEBUG
//		if(!Debug.isDebugBuild) return;
//		#endif
//		
//		string log = message.ToString();
//		log = String.Format("[{0:#,###0}ms] : {1}", ReadTimer("AppLaunch", true), message);
//		if(_textLog.Count >= MAXBUFFER) _textLog.RemoveAt(0);
//		_textLog.Add( new LogObject(){ level = LogLevel.LOG, message = log } );
//	}
	
//	public static string FlushTextLog(){
//		#if !PLAYER_DEBUG
//		if(!Debug.isDebugBuild) return "";
//		#endif
//		
//		if(_textLog.Count == 0) return "";
//		string output = "";
//		
//		_textLog.Reverse();
//		//if(_logs.Count > MAXBUFFER) _logs.RemoveRange(MAXBUFFER, _logs.Count - MAXBUFFER);
//		_textLog.ForEach((line) => {
//			string color = "white";
//			switch(line.level){
//				case LogLevel.LOG: color = "white"; break;
//				case LogLevel.WARNING: color = "yellow"; break;
//				case LogLevel.ERROR: color = "#ff6666ff"; break;
//			}
//			output += string.Format("<color={0}>[{1}] {2}</color>\r\n", color, line.level.ToString(), line.message);
//		});
//		_textLog.Reverse();
//		//_logs.Clear();
//
//		return output;
//	}
	
}
