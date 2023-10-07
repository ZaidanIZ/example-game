using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManagerUI : MonoBehaviour
{
  [SerializeField] private RectTransform stagesUIParent;
  [SerializeField] private RectTransform stageItemUIPrefab;
  [SerializeField] private Color[] _stagesUIColors;
  private List<Image> _stageImages = new List<Image>();

  public void Init(int stageCount)
  {
    RectTransform _clone;

    for (int i = 0; i < stageCount; i++)
    {
      _clone = Instantiate(stageItemUIPrefab, Vector3.zero, Quaternion.identity);
      _clone.SetParent(stagesUIParent);
      _clone.localPosition = Vector3.zero;
      _clone.localScale = new Vector3(1, 1, 1);
      _stageImages.Add(_clone.GetComponent<Image>());
    }
  }

  public void SetActiveItemUI(int stageLevel)
  {
    if (stageLevel >= _stageImages.Count) return;

    for (int i = 0; i < stageLevel; i++)
    {
      _stageImages[i].color = _stagesUIColors[0];
    }

    _stageImages[stageLevel].color = _stagesUIColors[1];
  }
}