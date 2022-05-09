using Project.Logic;
using Project.Pool;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UIs
{
    internal class UIManager : MonoBehaviour
    {
        [field: SerializeField] private ColorPalette Palette { get; set; }
        [field: SerializeField] private GameObject PaletteUI { get; set; }
        [field: SerializeField] private Transform TabPrefab { get; set; }
        [field: SerializeField] private Transform TaskPrefab { get; set; }

        private TabUI CurrentTab { get; set; }
        private ClassPooler<Transform> PrefabsPooler { get; set; }

        #region Mono

        private void Start()
        {
            Transform content = PaletteUI.transform.GetChild(2);
            int count = content.childCount;
            for (int i = 0; i < count; i++)
            {
                content.GetChild(i).GetComponent<Image>().color = Palette[i];
            }
        }

        #endregion



        #region Colors

        //Called by the palette's buttons
        internal void SetTabColor(int index)
        {
            if (CurrentTab)
            {
                CurrentTab.SetTabColor(Palette[index]);
            }

            //TODO: Change all the tasks' colors if any
        }

        //Called by the Tab's change color btn
        internal void OpenPalettePanel()
        {
            PaletteUI.SetActive(true);
        }

        //Referenced in the Tab Prefab Button script OnClick
        internal void SetTabAsCurrent(TabUI tab)
        {
            CurrentTab = tab;

            //TODO: Disable all other select tab btns
        }

        #endregion
    }
}