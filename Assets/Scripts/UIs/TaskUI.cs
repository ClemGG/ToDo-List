using Project.Logic;
using Project.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UIs
{
    internal class TaskUI : MonoBehaviour, IPooled
    {
        private TMP_InputField Field { get; set; }
        private Toggle Toggle { get; set; }
        private Image TaskColorImg { get; set; }
        private Button RemoveTaskBtn { get; set; }

        private UIManager Manager { get; set; }
        internal Task Task { get; private set; }

        public void Init()
        {
            Manager = FindObjectOfType<UIManager>();

            Field = GetComponentInChildren<TMP_InputField>();
            Toggle = GetComponentInChildren<Toggle>();
            TaskColorImg = transform.GetChild(0).GetComponentInChildren<Image>();
            RemoveTaskBtn = GetComponentInChildren<Button>();

            Field.onEndEdit.AddListener(delegate { SetTaskText(); });
            Toggle.onValueChanged.AddListener(delegate { SetTaskDone(); });
            RemoveTaskBtn.onClick.AddListener(RemoveTask);
        }


        //Called from the Manager when the Task GameObject is created
        internal void SetTask(Task task)
        {
            Task = task;
            Task.ID = transform.GetSiblingIndex() + 1;
            Field.text = task.Text;
            Toggle.isOn = task.Done;
        }

        //Called from the InputField
        internal void SetTaskText() => Task.Text = Field.text;

        //Called from the Toggle
        internal void SetTaskDone() => Task.Done = Toggle.isOn;

        //Called from the Manager when we set the Tab's color
        internal void SetTaskColor(Color32 color) => TaskColorImg.color = color;


        public void RemoveTask()
        {
            Manager.RemoveTask(this);
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