using UnityEngine.EventSystems;
using UnityEngine;

public class JoyButton : MonoBehaviour, IPointerUpHandler,IPointerDownHandler
{

    [HideInInspector]
   public bool Pressed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
}
