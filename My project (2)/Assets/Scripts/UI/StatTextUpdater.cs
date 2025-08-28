using TMPro;
using UnityEngine;

public class StatTextUpdater : MonoBehaviour
{
    [SerializeField] private string _baseText;
    [SerializeField] private TextMeshProUGUI _textMesh;

    private void Awake()
    {
        _textMesh.text = _baseText + "0";
    }

    public void UpdateText(int value)
    {
        _textMesh.text = _baseText + value.ToString();
    }
}
