using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    public static GameplayManager Instance;

    [SerializeField] private SpriteRenderer[] _environmentSprites;
    [SerializeField] private SpriteRenderer[] _insideEnvironmentSprites;
    [SerializeField] private FxManager _fxManager;
    [SerializeField] private GameObject OutSideEnvironment;
    [SerializeField] private GameObject InsideHerbalHut;
    [SerializeField] private CameraFollowAdvanced _cameraFollowAdvanced;
    [SerializeField] private CameraController _cameraController;


    private bool playerIsInsideHut = false;
    private bool disablePlayerControls = false;
    private bool isPlayerCollidingWithFlask = false;
    public bool PlayerIsInsideHut => playerIsInsideHut;
    public CameraController CameraController => _cameraController;

    public bool DisablePlayerControls { get => disablePlayerControls; set { disablePlayerControls = value; } }
    public bool IsPlayerCollidingWithFlask { get => isPlayerCollidingWithFlask; set { isPlayerCollidingWithFlask = value; } }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetEnvironmentToFade(bool shouldFade)
    {
        if(shouldFade)
        {
            _fxManager.FadeColor(_environmentSprites);
        }
        else
        {
            _fxManager.UnfadeColor(_environmentSprites);
        }
    }

    public void SetInsideEnvironmentToFade(bool shouldFade)
    {
        if (shouldFade)
        {
            _fxManager.FadeColor(_insideEnvironmentSprites);
        }
        else
        {
            _fxManager.UnfadeColor(_insideEnvironmentSprites);
        }
    }

    public void SetOutSideActive()
    {
        OutSideEnvironment.SetActive(true);
        InsideHerbalHut.SetActive(false);
        _cameraFollowAdvanced.enabled = true;
        playerIsInsideHut = false;
    }

    public void SetInsideHutActive()
    {
        playerIsInsideHut = true;
        OutSideEnvironment.SetActive(false);
        InsideHerbalHut.SetActive(true);
        _cameraFollowAdvanced.enabled = false;
        _cameraController.transform.position = new Vector3(0, 1, -10);
    }
}
