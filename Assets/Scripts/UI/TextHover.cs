using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler {
	public  Color hoverColor = Color.white;
	private Color _defaultColor = Color.black;

	private Text _textComponent;
	private Selectable _selectableComponent;

	public void Start() {
		_textComponent = GetComponent<Text>();
		_selectableComponent = GetComponent<Selectable>();
		_defaultColor = _textComponent.color;
	}

	public void OnPointerEnter(PointerEventData ed) {
		if(_textComponent != null) _textComponent.color = hoverColor;
		if(_selectableComponent != null) _selectableComponent.Select();
	}

	public void OnPointerExit(PointerEventData ed) {
		if(_textComponent != null) _textComponent.color = _defaultColor;
	}

	public void OnSelect(BaseEventData ed) {
		if(_textComponent != null) _textComponent.color = hoverColor;
	}

	public void OnDeselect(BaseEventData ed) {
		if(_textComponent != null) _textComponent.color = _defaultColor;
	}
}
