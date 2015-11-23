using System;
using UnityEngine;
using UnityEngine.UI;

public class MainInterfaceController : MonoBehaviour
{
	#region Properties
	#region Public
	public Text UISteps;
	public Text UITime;

	public static MainInterfaceController Instance;

	public event EventHandler RestartEvent;

	public int Steps
	{
		get
		{
			return _time;
		}
		set
		{
			_time = value;
			UISteps.text = "Steps: " + _time;
		}
	}
	public int Time
	{
		get
		{
			return _steps;
		}
		set
		{
			_steps = value;
			UITime.text = "Time: " + _steps + " sec";
		}
	}
	#endregion
	#region Private
	private int _steps;
	private int _time;
	#endregion
	#endregion

	#region Methods
	#region Public
	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void OnClickRestart()
	{
		if (RestartEvent == null)
			return;
		RestartEvent(this, null);

		gameObject.SetActive(false);
	}
	#endregion
	#region Private
	private void Awake()
	{
		Instance = this;
		gameObject.SetActive(false);
	}
	#endregion
	#endregion
}