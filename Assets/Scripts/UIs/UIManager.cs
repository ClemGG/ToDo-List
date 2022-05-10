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
        [field: SerializeField] private GameObject TabPrefab { get; set; }
        [field: SerializeField] private GameObject TaskPrefab { get; set; }
        [field: SerializeField] private Transform TabContent { get; set; }
        [field: SerializeField] private Transform TaskContent { get; set; }
        [field: SerializeField] private Button AddTabBtn { get; set; }
        [field: SerializeField] private Button AddTaskBtn { get; set; }
        [field: SerializeField] private int TaskMaxCount { get; set; } = 50;
        [field: SerializeField] private int TabMaxCount { get; set; } = 20;



        private TabUI CurrentTab { get; set; }
        private ClassPooler<GameObject> PrefabsPooler { get; set; }

        #region Mono

        private void Start()
        {
            //Setup color palette
            Transform content = PaletteUI.transform.GetChild(2);
            int count = content.childCount;
            for (int i = 0; i < count; i++)
            {
                content.GetChild(i).GetComponent<Image>().color = Palette[i];
            }

            //Setup Tabs and Tasks pooler
            PrefabsPooler = new ClassPooler<GameObject>
                (
                    new Pool<GameObject>(TabPrefab.name,
                                         TabMaxCount,
                                         () =>
                                         {
                                             TabUI tabUI = Instantiate(TabPrefab, TabContent).GetComponent<TabUI>();
                                             tabUI.Init();
                                             return tabUI.gameObject;
                                         }
                                         ),
                    new Pool<GameObject>(TaskPrefab.name,
                                         TaskMaxCount,
                                         () =>
                                         {
                                             TaskUI taskUI = Instantiate(TaskPrefab, TaskContent).GetComponent<TaskUI>();
                                             taskUI.Init();
                                             return taskUI.gameObject;
                                         }
                                         )
                );

            //Setup buttons
            AddTabBtn.onClick.AddListener(AddTab);
            AddTaskBtn.onClick.AddListener(AddTask);
        }

        #endregion


        #region Tabs

        //All of these are called from the TabUI

        internal void AddTab()
        {
            if (TabContent.childCount == TabMaxCount) return;

            TabUI tabUI = PrefabsPooler.GetFromPool<GameObject>(TabPrefab.name).GetComponent<TabUI>();
            tabUI.transform.SetParent(TabContent);
            tabUI.name = $"Onglet {tabUI.transform.GetSiblingIndex() + 1}";

            tabUI.SetTab(new Tab());
            tabUI.ChangeTabColor(Palette.GetRandomColor());

        }

        internal void RemoveTab(TabUI tabUI)
        {
            PrefabsPooler.ReturnToPool(tabUI.gameObject, TabPrefab.name);
            tabUI.transform.SetParent(transform);


            if (tabUI == CurrentTab)
            {

                int count = TaskContent.childCount;
                while(count > 0)
                {
                    RemoveTask(TaskContent.GetChild(count-1).GetComponent<TaskUI>());
                    count--;
                }

                CurrentTab = null;
            }
        }


        //Referenced in the Tab Prefab Button script OnClick
        internal void SetTabAsCurrent(TabUI tab)
        {


            //Disable all other select tab btns
            int count = TabContent.childCount;
            for (int i = 0; i < count; i++)
            {
                TabUI otherTab = TabContent.GetChild(i).GetComponent<TabUI>();
                if(otherTab != tab)
                {
                    otherTab.SetCurrentTabBtn.gameObject.SetActive(true);
                }
            }

            //Hides all previous tasks and displays the current ones
            count = TaskContent.childCount;
            while(count > 0)
            {
                GameObject taskUI = TaskContent.GetChild(count - 1).gameObject;
                PrefabsPooler.ReturnToPool(taskUI, TaskPrefab.name); 
                taskUI.transform.SetParent(transform);
                count--;
            }


            CurrentTab = tab;
            count = CurrentTab.Tab.Tasks.Count;


            for (int i = 0; i < count; i++)
            {
                TaskUI taskUI = PrefabsPooler.GetFromPool<GameObject>(TaskPrefab.name).GetComponent<TaskUI>();
                taskUI.transform.SetParent(TaskContent);

                taskUI.SetTask(CurrentTab.Tab.Tasks[i]);
                taskUI.SetTaskColor(tab.Tab.Color);
            }

        }

        #endregion



        #region Tasks

        //Both are called from the TaskUI

        internal void AddTask()
        {
            if (TaskContent.childCount == TaskMaxCount || CurrentTab is null) return;

            TaskUI taskUI = PrefabsPooler.GetFromPool<GameObject>(TaskPrefab.name).GetComponent<TaskUI>();
            taskUI.transform.SetParent(TaskContent);


            taskUI.SetTask(new Task());
            taskUI.SetTaskColor(CurrentTab.Tab.Color);
            CurrentTab.AddTask(taskUI.Task);

        }

        internal void RemoveTask(TaskUI taskUI)
        {
            CurrentTab.RemoveTask(taskUI.Task);
            PrefabsPooler.ReturnToPool(taskUI.gameObject, TaskPrefab.name);
            taskUI.transform.SetParent(transform);
        }

        #endregion



        #region Colors

        //Called by the palette's buttons
        internal void SetTabColor(int index)
        {
            if (CurrentTab)
            {
                CurrentTab.ChangeTabColor(Palette[index]);
            }

            //Change all the tasks' colors if any
            int count = TaskContent.childCount;
            for (int i = 0; i < count; i++)
            {
                TaskContent.GetChild(i).GetComponent<TaskUI>().SetTaskColor(CurrentTab.Tab.Color);
            }
        }

        //Called by the Tab's change color btn
        internal void OpenPalettePanel()
        {
            PaletteUI.SetActive(true);
        }

        #endregion
    }
}