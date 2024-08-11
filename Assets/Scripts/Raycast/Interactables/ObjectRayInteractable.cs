using UnityEngine;

namespace VoodooMagic.Scripts.RayInteraction.Interactables
{

  public class ObjectRayInteractable : RayInteractable
  {
    [Header("Hover Material")]
    [SerializeField] private Material hoverMaterial;

    [Header("Press Material")]
    [SerializeField] private Material pressMaterial;

    private Material originalMaterial;

    private void Start()
    {
      originalMaterial = gameObject.GetComponent<Renderer>().material;
    }

    public override void OnHover()
    {
      gameObject.GetComponent<Renderer>().material = hoverMaterial;
    }

    public override void RemoveHover()
    {
      base.RemoveHover();
      gameObject.GetComponent<Renderer>().material = originalMaterial;
    }

    protected override void OnPressed()
    {
      base.OnPressed();
      gameObject.GetComponent<Renderer>().material = pressMaterial;
    }
  }
}