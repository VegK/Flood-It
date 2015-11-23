using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AdditiveScene : MonoBehaviour
{
	#region Properties
	#region Public
	public string Scene = "MainInterface";

	public LoadEvent AfterLoad = new LoadEvent();
	#endregion
	#region Private

	#endregion
	#endregion

	#region Methods
	#region Public

	#endregion
	#region Private
	private IEnumerator Start()
	{
		yield return Application.LoadLevelAdditiveAsync(Scene);
		AfterLoad.Invoke();
	}
	#endregion
	#endregion
}

[Serializable]
public class LoadEvent : UnityEvent
{

}