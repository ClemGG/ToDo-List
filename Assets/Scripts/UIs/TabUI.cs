using Project.Logic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Project.Pool;

namespace Project.UIs
{
    internal class TabUI : MonoBehaviour, IPooled
    {
        private TMP_InputField Field { get; set; }
        internal Image ColorImg { get; set; }
        private Button ChangeColorBtn { get; set; }
        private Button RemoveTabBtn { get; set; }
        internal Button SetCurrentTabBtn { get; set; }

        private UIManager Manager { get; set; }
        internal Tab Tab { get; private set; }


        public void Init()
        {
            Manager = FindObjectOfType<UIManager>();

            Field = GetComponentInChildren<TMP_InputField>();
            ColorImg = transform.GetChild(1).GetComponentInChildren<Image>();
            ChangeColorBtn = transform.GetChild(2).GetChild(0).GetComponentInChildren<Button>();
            RemoveTabBtn = transform.GetChild(2).GetChild(2).GetComponentInChildren<Button>();
            SetCurrentTabBtn = transform.GetChild(3).GetComponentInChildren<Button>();

            Field.onEndEdit.AddListener(delegate { SetTabTitle(); });
            ChangeColorBtn.onClick.AddListener(OpenPalettePanel);
            RemoveTabBtn.onClick.AddListener(RemoveTab);
            SetCurrentTabBtn.onClick.AddListener(SetTabAsCurrent);

        }




        //Called from the Manager when the Tab GameObject is created
        internal void SetTab(Tab tab) 
        {
            Tab = tab;
            Tab.ID = transform.GetSiblingIndex() + 1;

            SetTabTitle(name);
            SetTabAsCurrent();
        }

        //Called from the InputField
        internal void SetTabTitle() => Tab.Title = Field.text;

        //Called from the Manager
        internal void SetTabTitle(string title) => Tab.Title = Field.text = title;

        //Called from the Manager
        internal void ChangeTabColor(Color32 color)
        {
            Tab.Color = color;
            ColorImg.color = Tab.Color;
        }



        internal void AddTask(Task task)
        {
            Tab.Tasks.Add(task);
        }

        internal void RemoveTask(Task task)
        {
            Tab.Tasks.Remove(task);
        }


        internal void OpenPalettePanel()
        {
            Manager.OpenPalettePanel();
        }

        internal void SetTabAsCurrent()
        {
            SetCurrentTabBtn.gameObject.SetActive(false);
            Manager.SetTabAsCurrent(this);
        }

        internal void RemoveTab()
        {
            Manager.RemoveTab(this);
            Tab.Tasks.Clear();
        }


        public void OnEnqueued()
        {
            gameObject.SetActive(false);
        }

        public void OnDequeued()
        {
            gameObject.SetActive(true);
        }
    }
}