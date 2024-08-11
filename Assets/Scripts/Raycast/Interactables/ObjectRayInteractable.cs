using UnityEngine;

namespace VoodooMagic.Scripts.RayInteraction.Interactables
{

  public class ObjectRayInteractable : RayInteractable
  {
    [Header("Highlight Material")]
    [SerializeField] private Material highlightMaterial;
    private Material originalMaterial;

    private void Start()
    {
      originalMaterial = gameObject.GetComponent<Renderer>().material;
    }

    public override void highlight()
    {
      gameObject.GetComponent<Renderer>().material = highlightMaterial;
    }

    public override void removeHighlight()
    {
      gameObject.GetComponent<Renderer>().material = originalMaterial;
    }
  }
}