using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{

    public List<TabButton> tab_buttons;
    public Sprite tab_idle;
    public Sprite tab_hover;
    public Sprite tab_selected;
    public TabButton active_tab;

    public List<GameObject> pages;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Subscribe(TabButton button) 
    { 
        if(tab_buttons == null) 
        {
            tab_buttons = new List<TabButton>();
        }

        tab_buttons.Add(button);
    }

    public void OnTabEnter(TabButton button) 
    {
        ResetTabs();
        if (tab_selected == null || button != active_tab)
        {
            button.background.sprite = tab_hover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }
    public void OnTabSelect(TabButton button)
    {
        active_tab = button;
        ResetTabs();
        button.background.sprite = tab_selected;
        int index = button.transform.GetSiblingIndex();

        for (int i = 0; i < pages.Count; i++) 
        {
            if (i == index)
            {
                pages[i].SetActive(true);
            }
            else 
            {
                pages[i].SetActive(false);
            }
        }
    }

    public void ResetTabs() 
    {
        foreach (TabButton button in tab_buttons) 
        {
            if(tab_selected != null && button == active_tab) 
            { 
                continue; 
            }

            button.background.sprite = tab_idle;
        }
    }
}
