using UnityEngine;
using System.Collections;

public class EnemyManager {

	private static EnemyManager _instance = null;

	public static EnemyManager GetInstance(){
		if(_instance == null){
			_instance = new EnemyManager();

			//_instance.Setup();
		}

		return _instance;
	}

	public void Setup(){
		
		
	}
	
	public void AddEnemy(){


	}

	//public void 

}
