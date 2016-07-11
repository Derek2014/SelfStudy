using UnityEngine;
using System.Collections;

public class SimpleButton : MonoBehaviour {

	public TextMesh tm_title;

	public void Setup(string title, Vector2 position){

		tm_title.text = title;

		this.transform.localPosition = new Vector3(position.x, position.y, 0);
	}

}
