using UnityEngine;
using Project.UIs;
using Project.Logic;
using System.Collections.Generic;

namespace Project.Save
{
    public class BinarySerializationManager : MonoBehaviour
    {
        [field: SerializeField] public string FileName { get; set; } = "Tab";

        private UIManager Manager { get; set; }
        private List<Tab> TabsToSave { get; set; }

        private void Start()
        {
            Manager = FindObjectOfType<UIManager>();
            TabsToSave = new List<Tab>(Manager.TabMaxCount);

            Manager.OnTabCreated += AddTab;
            Manager.OnTabRemoved += RemoveTab;
            Manager.OnTabsLoaded += Load;

            Application.quitting += Save;
        }

        private void AddTab(Tab tab)
        {
            TabsToSave.Add(tab);
        }

        private void RemoveTab(Tab tab)
        {
            TabsToSave.Remove(tab);
        }

        private IEnumerable<Tab> Load()
        {
            string[] savedFiles = BinarySerializer.GetAllSaveFileNames();

            for (int i = 0; i < savedFiles.Length; i++)
            {
                string path = string.Format("{0}/{1}{2}", BinarySerializer.saveFolderPath, $"{FileName}{i + 1}", BinarySerializer.saveFileExtension);
                yield return BinarySerializer.Load<Tab>(path);
            }
            
        }

        private void Save()
        {
            BinarySerializer.DeleteAllSaveFiles();

            for (int i = 0; i < TabsToSave.Count; i++)
            {
                BinarySerializer.Save($"{FileName}{i+1}", TabsToSave[i]);
            }
        }

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Return))
        //    {
        //        if (BinarySerializer.Save(FileName, dataToSave))
        //        {
        //            string path = string.Format("{0}/{1}{2}", BinarySerializer.saveFolderPath, FileName, BinarySerializer.saveFileExtension);
        //            dataToLoad = BinarySerializer.Load<BinarySaveData>(path);
        //        }
        //    }

        //    if (Input.GetKeyDown(KeyCode.Backspace))
        //    {
        //        BinarySerializer.DeleteAllSaveFiles();
        //    }
        //    if (Input.GetKeyDown(KeyCode.Delete))
        //    {
        //        BinarySerializer.DeleteSaveFileAtPath(FileName, BinarySerializer.saveFolderPath, BinarySerializer.saveFileExtension);
        //    }
        //    if (Input.GetKeyDown(KeyCode.RightShift))
        //    {
        //        string[] savedFiles = BinarySerializer.GetAllSaveFileNames();

        //        if (savedFiles.Length == 0f)
        //        {
        //            print($"Le dossier \"{BinarySerializer.saveFolderPath}\" est vide.");
        //        }
        //        else
        //        {
        //            for (int i = 0; i < savedFiles.Length; i++)
        //            {
        //                print(savedFiles[i] + "\n");
        //            }
        //        }

        //    }
        //}
    }
}