using InputReader;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExternalDevicesInputReader : IEntityInputSource
{
    public float HorizontalDirection => Input.GetAxisRaw("Horizontal");
    
    public float VerticalDirection => Input.GetAxisRaw("Vertical");
    
    public  bool Jump { get; private set; }

    public void OnUpdate()
    {
        if (Input.GetButtonDown("Jump"))
            Jump = true;
    }

    private bool IsPointerOverUi() => EventSystem.current.IsPointerOverGameObject();
    
    public void ResetOneTimeActions()
    {
        Jump = false;
    }
}