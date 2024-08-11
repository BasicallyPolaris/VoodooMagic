using UnityEngine;
using UnityEngine.UI;

namespace VoodooMagic.Scripts.RayInteraction.Interactables
{

  public class UIRayInteractable : RayInteractable
  {
    [Header("Image Highlight")]
    [SerializeField] private Image image;

    [Header("Hover Color")]
    [SerializeField] private Color hoverColor;

    [Header("Press Color")]
    [SerializeField] private Color pressColor;

    private Color originalColor;

    private void Start()
    {
      originalColor = gameObject.GetComponent<Image>().color;
    }

    public override void OnHover()
    {
      gameObject.GetComponent<Image>().color = hoverColor;
    }

    public override void RemoveHover()
    {
      base.RemoveHover();
      gameObject.GetComponent<Image>().color = originalColor;
    }

    protected override void OnPressed()
    {
      base.OnPressed();
      gameObject.GetComponent<Image>().color = pressColor;
    }
  }
}