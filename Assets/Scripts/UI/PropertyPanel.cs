using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyPanel : MonoBehaviour
{

    public float defaultValue;
    public Vector2 valueMinMax;
    float value = float.MaxValue;
    float maxDeltaPerTick;

    public Slider slider;
    public InputField inputField;

    public bool integer = false;

    private void Start () {
        if (value == float.MaxValue)
            value = defaultValue;
#if UNITY_EDITOR
        maxDeltaPerTick = (valueMinMax.y - valueMinMax.x) * Time.fixedDeltaTime / 3f; // (value range) * (delta time) / (seconds for full deflection)
#else
        maxDeltaPerTick = (valueMinMax.y - valueMinMax.x) * Time.fixedDeltaTime;
#endif
        inputField.text = "" + value;
    }

    private void Update () {

        if (slider.value != 0) {
            value += slider.value * maxDeltaPerTick;
            value = Mathf.Clamp (value, valueMinMax.x, valueMinMax.y);
            inputField.text = value.ToString (!integer?"F3":"F0");
        }

        if (!integer) {
            if (Mathf.Round (float.Parse (inputField.text) * 100f) / 100f != Mathf.Round (value * 100f) / 100f) {
                value = float.Parse (inputField.text);
                value = Mathf.Clamp (value, valueMinMax.x, valueMinMax.y);
            }
        } else {
            if (Mathf.RoundToInt (float.Parse (inputField.text)) != Mathf.RoundToInt(value)) {
                value = float.Parse (inputField.text);
                value = Mathf.Clamp (value, valueMinMax.x, valueMinMax.y);
            }
        }
    }

    public float GetValue () => (value != float.MaxValue) ? (integer ? Mathf.RoundToInt(value) : value) : defaultValue;

    public void SetValue (float v) {
        value = v;
        inputField.text = v.ToString (!integer ? "F2" : "F0");
    }
}
