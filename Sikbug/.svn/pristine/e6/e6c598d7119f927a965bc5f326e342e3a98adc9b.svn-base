using System;
using System.Windows;
using System.IO.IsolatedStorage;
using System.IO;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace Sikbug.Utils
{
    public class IsolatedStorage
    {
        private static Object _thisLock = new Object();

        public static T LoadSetting<T>(string fileName)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!store.FileExists(fileName))
                    return default(T);

                lock (_thisLock)
                {
                    try
                    {
                        using (var stream = store.OpenFile(fileName, FileMode.Open, FileAccess.Read))
                        {
                            var serializer = new DataContractSerializer(typeof(T));
                            return (T)serializer.ReadObject(stream);
                        }
                    }
                    catch (SerializationException se)
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(
                            () => MessageBox.Show(String.Format("Serialize file error {0}:{1}", se.Message, fileName)));
                        return default(T);
                    }
                    catch (Exception e)
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(
                            () => MessageBox.Show(String.Format("Load file error {0}:{1}", e.Message, fileName)));
                        return default(T);
                    }
                }
            }
        }

        public static void SaveSetting<T>(string fileName, T dataToSave)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                lock (_thisLock)
                {
                    try
                    {
                        using (var stream = store.CreateFile(fileName))
                        {
                            var serializer = new DataContractSerializer(typeof(T));
                            serializer.WriteObject(stream, dataToSave);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(String.Format("Save file error {0}:{1}", e.Message, fileName));
                        return;
                    }
                }
            }
        }

        public static void DeleteFile(string fileName)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    if (store.FileExists(fileName))
                        store.DeleteFile(fileName);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception is : " + ex.Message);
                }
            }
        }
    }
}
