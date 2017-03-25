using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler {
	public  Color hoverColor = Color.white;
	private Color _defaultColor = Color.black;
	private Text  _textComponent;

	public void Start() {
		_textComponent = GetComponent<Text>();
		_defaultColor = _textComponent.color;
	}

	public void OnPointerEnter(PointerEventData ed) {
		_textComponent.color = hoverColor;
		if(GetComponent<Selectable>() != null) GetComponent<Selectable>().Select();
	}

	public void OnSelect(BaseEventData ed) {
		_textComponent.color = hoverColor;
	}

	public void OnPointerExit(PointerEventData ed) {
		_textComponent.color = _defaultColor;
	}

	public void OnDeselect(BaseEventData ed) {
		_textComponent.color = _defaultColor;
	}
}
