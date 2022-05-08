using Project.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UIs
{
    internal class TaskUI : MonoBehaviour
    {
        private InputField Field { get; set; }
        private Toggle Toggle { get; set; }
        private Image TaskColor { get; set; }
        private Button RemoveTaskBtn { get; set; }

        private Task Task { get; set; }

        private void Start()
        {
            Field = GetComponentInChildren<InputField>();
            Toggle = GetComponentInChildren<Toggle>();
            TaskColor = transform.GetChild(0).GetComponentInChildren<Image>();
            RemoveTaskBtn = GetComponentInChildren<Button>();


            Field.onEndEdit.AddListener(delegate { SetTaskTitle(); });
            Toggle.onValueChanged.AddListener(delegate { SetTaskDone(); });
            RemoveTaskBtn.onClick.AddListener(RemoveTask);
        }

        //Called from the Manager when the Task GameObject is created
        internal void SetTask(Task task)
        {
            Task = task;
            Task.SetID(transform.GetSiblingIndex());
        }

        //Called from the InputField
        internal void SetTaskTitle() => Task.SetText(Field.text);

        //Called from the Toggle
        internal void SetTaskDone() => Task.SetDone(Toggle.isOn);

        //Called from the Manager when we set the Tab's color
        internal void SetTaskColor(Color32 color) => TaskColor.color = color;


        public void RemoveTask()
        {
            //TODO
        }
    }
}