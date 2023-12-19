using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.General;
using UnityEngine;

namespace UI.Windows
{
    public class UIWindowMaster : Singleton<UIWindowMaster>
    {
        [SerializeField] private UIWindowHandler standardWindowToOpen;
        [SerializeField] private bool handelCursor;
        [SerializeField] private bool menuActive;
        [SerializeField] private bool enableSystem = true;
        [SerializeField] private bool stopTimeIfUIActive = true;
        
        public Action<bool> OnActiveUIChanged;
        public readonly List<UIWindowHandler> CurrentlyActiveWindows = new ();

        private void Start()
        {
            AudioOptionsUIWindow audioOptions = FindObjectOfType<AudioOptionsUIWindow>(true);
            if (audioOptions) audioOptions.LoadFromSaveText();
            if(handelCursor) CursorManager.Instance.DeActivateCursor();
        }

        private void OnEnable()
        {
            Time.timeScale = 1;
        }

        public bool MenuActive
        {
            get => menuActive;
            set
            {
                if (!value == menuActive)
                {
                    menuActive = value;
                    OnActiveUIChanged?.Invoke(menuActive);
                }
            }
        }


        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                if (!MenuActive) OpenWindow(); 
                else PopWindowHandler().UIEsc();
            }
        }

        public void PushWindowHandler(UIWindowHandler handler)
        {
            if (CurrentlyActiveWindows.Contains(handler)) CurrentlyActiveWindows.Remove(handler);
            CurrentlyActiveWindows.Insert(0,handler);
        }

        public UIWindowHandler PopWindowHandler()
        {
            UIWindowHandler handler = CurrentlyActiveWindows[0];
            CurrentlyActiveWindows.RemoveAt(0);
            return handler;
        }

        public void OpenWindow(UIWindowHandler windowToOpen = null)
        {
            if(!enableSystem) return;
            if (windowToOpen == null) windowToOpen = standardWindowToOpen;
            windowToOpen.ActivateWindow();
          
            MenuActive = true;
        }

        public void UpdateState()
        {
            StartCoroutine(UpdateMenuState());
        }
        
        private IEnumerator UpdateMenuState()
        {
            yield return new WaitForEndOfFrame();
            
            MenuActive = CurrentlyActiveWindows.Any();
            
            if (handelCursor)
            {
                if (menuActive)CursorManager.Instance.ActivateCursor();
                else CursorManager.Instance.DeActivateCursor();
            }
            if (stopTimeIfUIActive)
            {
                Time.timeScale = menuActive ? 0 : 1;
            }
        }
    }
}
