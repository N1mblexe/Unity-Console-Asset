using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DynamicTextResizer : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Scrollbar scrollBar;

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();

        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(OnTextChanged);
    }

    private void OnDestroy()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(OnTextChanged);
    }

    private void OnTextChanged(Object obj)
    {
        if (obj == textMeshProUGUI)
        {
            textMeshProUGUI.rectTransform.sizeDelta = new Vector2(textMeshProUGUI.rectTransform.sizeDelta.x, textMeshProUGUI.preferredHeight);
            try
            {
                scrollBar.value = 0;
            }
            catch (System.Exception ignored) { }
        }
    }
}