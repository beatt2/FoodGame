using System;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using Grid;
using TimeSystem;
using Tools;
using UnityEngine;

namespace Save
{
    public class SaveManager : Singleton<SaveManager>
    {
        public System.DateTime StopTime;

        private SaveInfo _saveInfo;

        protected override void Awake()
        {
            base.Awake();
            _saveInfo = LoadFile<SaveInfo>();
            
        }
        


        private void OnApplicationQuit()
        {

            
           _saveInfo  = new SaveInfo(DateTime.Now, TimeManager.Instance.GetMonth(), TimeManager.Instance.GetYear());
           SaveFiles(_saveInfo);
        }

        public DateTime GetStopTime()
        {
            return _saveInfo.StopTime;
        }

        public int GetSaveYear()
        {
            return _saveInfo.SaveYear;
        }

        public int GetSaveMonth()
        {
            return _saveInfo.SaveMonth;
        }
        

        public string Filename = "test";
        public string Extension = "save";

        public bool CheckFileExistance()
        {
            return File.Exists(GetPath(Filename, Extension));
        }

        public void SaveFiles<T>(T profile)
        {
            File.WriteAllBytes(GetPath(Filename, Extension), SerializeDate(profile));
        }

        public T LoadFile<T>()
        {
            byte[] data = File.ReadAllBytes(GetPath(Filename, Extension));
            MemoryStream ms = new MemoryStream(data);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            return (T) binaryFormatter.Deserialize(ms);
        }

        private static byte[] SerializeDate<T>(T profile)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(ms, profile);
            return ms.ToArray();
        }


        private static string GetPath(string filename, string extension)
        {
            return Application.persistentDataPath + "/" + filename + "." +extension;
        }






    }
}
