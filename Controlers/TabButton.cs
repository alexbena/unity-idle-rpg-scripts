using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabController tab_controller;

    public Image background;

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        tab_controller = transform.parent.GetComponent<TabController>();
        tab_controller.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // INTERFACE IMPLEMENTATIONS
    public void OnPointerClick(PointerEventData eventData)
    {
        tab_controller.OnTabSelect(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tab_controller.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tab_controller.OnTabExit(this);
    }

}
