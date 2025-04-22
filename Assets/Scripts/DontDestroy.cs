using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

	public static DontDestroy instance = null;

	private static int level = 0;

	void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else if (instance != this) {
			Destroy(gameObject);
		}
	}

	public int GetLevel()
	{
		return level;
	}

    public void ResetLevel()
    {
        level = 0;
    }

	public void IncreaseLevel()
	{
		level++;
	}
}
