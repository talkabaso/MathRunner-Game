using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerUpdate : MonoBehaviour {
	[SerializeField] Text timerText;	
	public static TimeSpan timeForResult;
	private float time;
	private float secondsCount;
	private int minuteCount;
	private int hourCount;

	void Update() {
		UpdateTimerUI();
	}

	public void UpdateTimerUI() { //set timer UI

		secondsCount += Time.deltaTime;
		timerText.text = hourCount + "h:" + minuteCount + "m:" + (int)secondsCount + "s";
		if (secondsCount >= 60) {
			minuteCount++;
			secondsCount = 0;
		}
		else if (minuteCount >= 60) {
			hourCount++;
			minuteCount = 0;
		}
		
		timeForResult = new TimeSpan(hourCount, minuteCount, (int)secondsCount);
	}
}