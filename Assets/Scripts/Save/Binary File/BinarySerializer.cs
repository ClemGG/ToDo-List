using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Project.Logic;
using UnityEngine;


namespace Project.Save
{
    public static class BinarySerializer
    {
        public static readonly string saveFolderPath = $"{Application.persistentDataPath}/Saves";
        public static readonly string saveFileExtension = ".save";


        public static string[] GetAllSaveFileNames(string folderPath = null, string fileExtension = null)
        {
            string[] savedFiles = null;
            string path = string.IsNullOrEmpty(folderPath) ? saveFolderPath : folderPath;

            if (Directory.Exists(path))
            {
                if (string.IsNullOrEmpty(fileExtension))
                {
                    savedFiles = Directory.GetFiles(path);
                }
                else
                {
                    savedFiles = Directory.GetFiles(path, $"*{fileExtension}");
                }
            }
            else
            {
                Debug.LogError($"Erreur : Le dossider de sauvegarde référencé n'existe pas : \"{path}\".");
            }

            return savedFiles;

        }


        public static void DeleteAllSaveFiles(string folderPath = null, string fileExtension = null)
        {
            string[] savedFiles = null;
            string path = string.IsNullOrEmpty(folderPath) ? saveFolderPath : folderPath;

            if (Directory.Exists(path))
            {
                if (string.IsNullOrEmpty(fileExtension))
                {
                    savedFiles = Directory.GetFiles(path);
                }
                else
                {
                    savedFiles = Directory.GetFiles(path, $"*{fileExtension}");
                }
            }
            else
            {
                Debug.LogWarning($"Attention : Le dossider de sauvegarde référencé n'existe pas : \"{path}\".");
                return;
            }

            for (int i = 0; i < savedFiles.Length; i++)
            {
                File.Delete(savedFiles[i]);
            }

            Debug.Log($"Tous les fichiers ont bien été supprimés à 'emplacement \"{path}\".");
        }


        public static void DeleteSaveFileAtPath(string fileName, string folderPath = null, string fileExtension = null)
        {
            string path = string.Format("{0}/{1}{2}", string.IsNullOrEmpty(folderPath) ? saveFolderPath : folderPath, fileName, fileExtension);

            if (File.Exists(path))
            {
                if (string.IsNullOrEmpty(fileExtension))
                {
                    Debug.LogError($"Erreur : L'extension du fichier à supprimer n'a pas été référencé : \"{path}\".");
                }
                else
                {
                    File.Delete(path);
                    Debug.Log($"Fichié supprimé : \"{path}\".");
                }
            }
            else
            {
                string folder = string.IsNullOrEmpty(folderPath) ? saveFolderPath : folderPath;
                Debug.LogError($"Erreur : Le fichier à supprimer \"{fileName}\" n'existe pas dans le dossier \"{folder}\".");
            }
        }



        public static bool Save(string saveFileName, object dataToSave)
        {
            bool saveCompleted = false;

            BinaryFormatter formatter = GetBinaryFormatter();

            if (!Directory.Exists(saveFolderPath))
            {
                Directory.CreateDirectory(saveFolderPath);
            }

            string path = string.Format("{0}/{1}{2}", saveFolderPath, saveFileName, saveFileExtension);
            FileStream file = File.Create(path);

            try
            {
                formatter.Serialize(file, dataToSave);
                saveCompleted = true;
                Debug.Log($"Fichier enregistré : \"{path}\".");
            }
            catch (Exception e)
            {
                //Debug.LogError($"{e.GetType()} : Les données n'ont pas pu être enregistrées au chemin \"{path}\".");
                Debug.LogError(e);
            }

            file.Close();


            return saveCompleted;
        }


        public static T Load<T>(string path)
        {
            if (!File.Exists(path))
            {
                Debug.LogError($"Erreur : Le fichier demandé n'existe pas à \"{path}\".");
                return default;
            }

            BinaryFormatter formatter = GetBinaryFormatter();

            FileStream file = File.OpenRead(path);
            T dataToLoad = default;

            try
            {
                dataToLoad = (T)formatter.Deserialize(file);
            }
            catch (Exception e)
            {
                Debug.LogError($"{e.GetType()} : Les données n'ont pas pu être chargées au chemin indiqué. Vérifiez que le chemin est correct : \"{path}\".");
            }

            file.Close();
            return dataToLoad;
        }
        
        private static BinaryFormatter GetBinaryFormatter()
        {
            BinaryFormatter formatter = new();
            formatter.SurrogateSelector = GetSurrogateSelector();

            return formatter;
        }

        private static SurrogateSelector GetSurrogateSelector()
        {
            SurrogateSelector selector = new SurrogateSelector();

            TabSerializationSurrogate tabSurrogate = new();
            Color32SerializationSurrogate color32Surrogate = new();

            selector.AddSurrogate(typeof(Tab), new StreamingContext(StreamingContextStates.All), tabSurrogate);
            selector.AddSurrogate(typeof(Color32), new StreamingContext(StreamingContextStates.All), color32Surrogate);

            return selector;

        }
    }
}