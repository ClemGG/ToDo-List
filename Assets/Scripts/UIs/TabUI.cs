using UnityEngine;
using UnityEngine.UI;

namespace Project.UIs
{
    internal class TabUI : MonoBehaviour
    {
        private InputField Field { get; set; }
        private Image ColorImg { get; set; }
        private Button ChangeColorBtn { get; set; }
        private Button RemoveTabBtn { get; set; }
        private Button SetCurrentTabBtn { get; set; }

        private UIManager Manager { get; set; }

        private void Start()
        {
            Manager = FindObjectOfType<UIManager>();

            Field = GetComponentInChildren<InputField>();
            ColorImg = transform.GetChild(1).GetComponentInChildren<Image>();
            ChangeColorBtn = transform.GetChild(2).GetChild(0).GetComponentInChildren<Button>();
            RemoveTabBtn = transform.GetChild(2).GetChild(2).GetComponentInChildren<Button>();
            SetCurrentTabBtn = transform.GetChild(3).GetComponentInChildren<Button>();

            ChangeColorBtn.onClick.AddListener(OpenPalettePanel);
            RemoveTabBtn.onClick.AddListener(RemoveTab);
            SetCurrentTabBtn.onClick.AddListener(SetTabAsCurrent);
        }

        internal string GetTabText()
        {
            return Field.text;
        }

        internal void OpenPalettePanel()
        {
            Manager.OpenPalettePanel();
        }

        internal void SetTabAsCurrent()
        {
            Manager.SetTabAsCurrent(this);
        }

        internal void SetColor(Color32 color)
        {
            ColorImg.color = color;
        }

        internal void RemoveTab()
        {
            //TODO
        }
    }
}