using UnityEngine;
using UnityEngine.InputSystem;


public interface IClickable
{
    public void OnClick();
}
public class MouseHandler : MonoBehaviour
{
    InputAction leftClick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftClick = InputSystem.actions.FindAction("Attack");
        leftClick.performed += ctx => DoLeftClick();
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
    IClickable CheckHit(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            return hit.collider.gameObject.GetComponent<MonoBehaviour>() as IClickable;
        }

        return null;
    }
}
