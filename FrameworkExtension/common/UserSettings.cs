using System;

namespace Automation.FrameworkExtension.common
{

    public interface IObjSerializer
    {

        object ReadXML(string file, Type t);

        void WriteXML(object obj, string file, Type t);


    }



    public abstract class UserSettings<T> where T : class
    {

        public static IObjSerializer Serializer;


        private static object obj = new object();
        public static T Load(string file)
        {
            lock (obj)
            {
                return Serializer.ReadXML(file, typeof(T)) as T;
            }
        }

        public virtual void Save(string environment)
        {
            Serializer.WriteXML(this, environment, this.GetType());
        }

        public virtual void SaveAs(string environment)
        {
            Serializer.WriteXML(this, environment, typeof(T));
        }
    }
}