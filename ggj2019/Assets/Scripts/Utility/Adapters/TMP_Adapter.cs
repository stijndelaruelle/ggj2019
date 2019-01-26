using UnityEngine;
using TMPro;

public class TMP_Adapter : BaseTextAdapter
{
	private TextMeshPro textComponent;

	private TextMeshProUGUI textUIComponent;

	protected override void Awake()
	{
		base.Awake();

		textComponent = GetComponent<TextMeshPro>();
		textUIComponent = GetComponent<TextMeshProUGUI>();

		Debug.Assert(textComponent != null || textUIComponent != null, "No TMP text component attached", this);

		UpdateText();
	}

	protected override void ApplyText(string text)
	{
		if (textComponent != null)
			textComponent.text = text;

		if (textUIComponent != null)
			textUIComponent.text = text;
	}
}
