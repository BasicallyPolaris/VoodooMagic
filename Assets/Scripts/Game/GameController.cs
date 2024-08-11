using UnityEngine;
using VoodooMagic.Scripts.Controlls;

namespace VoodooMagic.Scripts.Game
{
  public enum GamePhase
  {
    Start,
    Preparation,
    Play,
    End
  }

  public class GameController : MonoBehaviour
  {
    [Header("Controll-System")]
    [SerializeField] private ControllManager controllManager;

    private GamePhase currentPhase = GamePhase.Start;
    private Canvas preparationCanvas;
    private Canvas startCanvas;

    public void Awake()
    {
      if (controllManager == null)
      {
        Debug.LogError("Controll Manager is not set.");
        return;
      }

      preparationCanvas = GameObject.Find("PreparationUICanvas").GetComponent<Canvas>();
      startCanvas = GameObject.Find("StartUICanvas").GetComponent<Canvas>();
    }

    public void NextPhase()
    {
      switch (currentPhase)
      {
        case GamePhase.Start:
          currentPhase = GamePhase.Preparation;
          PreparationPhase();
          break;
        case GamePhase.Preparation:
          currentPhase = GamePhase.Play;
          PlayPhase();
          break;
        case GamePhase.Play:
          currentPhase = GamePhase.End;
          EndPhase();
          break;
        case GamePhase.End:
          currentPhase = GamePhase.Start;
          StartPhase();
          break;
        default:
          Debug.LogError("Unknown GamePhase: " + currentPhase);
          break;
      }
    }

    private void StartPhase()
    {
      controllManager.SetControllerState(ControllerState.Idle);
    }

    private void PreparationPhase()
    {
      controllManager.SetControllerState(ControllerState.SoftPatchInteraction);
      preparationCanvas.enabled = true;
    }

    private void PlayPhase()
    {
      controllManager.SetControllerState(ControllerState.Idle);
      preparationCanvas.enabled = false;
      startCanvas.enabled = false;
    }

    private void EndPhase()
    {
      controllManager.SetControllerState(ControllerState.Idle);
    }
  }
}