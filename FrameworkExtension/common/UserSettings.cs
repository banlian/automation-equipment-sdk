using System;
using System.IO;
using System.Xml.Serialization;

namespace Automation.FrameworkExtension.common
{
    public abstract class UserSettings<T> where T : class
    {
        private static object obj = new object();
        public static T Load(string file)
        {
            lock (obj)
            {
                using (var fs = new FileStream(file, FileMode.Open))
                {
                    return new XmlSerializer(typeof(T)).Deserialize(fs) as T;
                }
           
            }
        }

        public static T Load(string file, Type t)
        {
            lock (obj)
            {
                using (var fs = new FileStream(file, FileMode.Open))
                {
                    return new XmlSerializer(t).Deserialize(fs) as T;
                }
            }
        }

        public virtual void Save(string file)
        {
            using (var fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                new XmlSerializer(typeof(T)).Serialize(fs, this);
            }
        }

        public abstract bool CheckIfNormal();
    }
}