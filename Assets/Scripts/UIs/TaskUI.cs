using UnityEngine;
using UnityEngine.UI;

namespace Project.UIs
{
    internal class TaskUI : MonoBehaviour
    {
        private InputField Field { get; set; }
        private Toggle Toggle { get; set; }
        private Button RemoveTaskBtn { get; set; }


        private void Start()
        {
            Field = GetComponentInChildren<InputField>();
            Toggle = GetComponentInChildren<Toggle>();
            RemoveTaskBtn = GetComponentInChildren<Button>();
            RemoveTaskBtn.onClick.AddListener(RemoveTask);
        }

        public string GetTaskText()
        {
            return Field.text;
        }

        public bool GetTaskState()
        {
            return Toggle.isOn;
        }

        public void RemoveTask()
        {
            //TODO
        }
    }
}