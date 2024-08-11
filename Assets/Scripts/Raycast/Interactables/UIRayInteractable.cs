using UnityEngine;
using UnityEngine.UI;

namespace VoodooMagic.Scripts.RayInteraction.Interactables
{

  public class UIRayInteractable : RayInteractable
  {
    [Header("Image Highlight")]
    [SerializeField] private Image image;

    [Header("Highlight Color")]
    [SerializeField] private Color highlightColor;
    private Color originalColor;

    private void Start()
    {
      originalColor = gameObject.GetComponent<Image>().color;
    }

    public override void highlight()
    {
      gameObject.GetComponent<Image>().color = highlightColor;
    }

    public override void removeHighlight()
    {
      gameObject.GetComponent<Image>().color = originalColor;
    }
  }
}