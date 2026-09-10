using UnityEngine;
using UnityEngine.InputSystem;


public interface IClickable
{
    public void OnClick();
    public void OnRightClick();
}
public class MouseHandler : MonoBehaviour
{
    InputAction leftClick, rightClick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftClick = InputSystem.actions.FindAction("Attack");
        leftClick.performed += ctx => DoLeftClick();

        rightClick = InputSystem.actions.FindAction("RightClick");
        rightClick.performed += ctx => DoRightClick();
    }

    public void DoLeftClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero);
        IClickable obj = CheckHit(hit);

        if (obj != null)
        {
            obj.OnClick();
        }
    }

    public void DoRightClick() 
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero);
        IClickable obj = CheckHit(hit);

        if (obj != null)
        {
            obj.OnRightClick();
        }
    }
    IClickable CheckHit(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            return hit.collider.gameObject.GetComponent<MonoBehaviour>() as IClickable;
        }

        return null;
    }
}
