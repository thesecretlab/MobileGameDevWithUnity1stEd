using UnityEngine;
using System.Collections;

// BEGIN 3d_indicatormanager
using UnityEngine.UI;

public class IndicatorManager : Singleton<IndicatorManager> {
	
	public RectTransform labelContainer;
	
	public Indicator indicatorPrefab;
	
	public Indicator AddLabel(GameObject target, Color color, Sprite sprite = null) {
		
		var newLabel = Instantiate(indicatorPrefab);
		
		newLabel.target = target.transform;
		newLabel.color = color;

		if (sprite != null) {
			newLabel.GetComponent<Image>().sprite = sprite;
		}
		
		newLabel.transform.SetParent(labelContainer, false);
		
		return newLabel;
	}
	
}
// END 3d_indicatormanager