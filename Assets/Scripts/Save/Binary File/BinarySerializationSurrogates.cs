using System.Runtime.Serialization;
using UnityEngine;
using Project.Logic;
using System.Collections.Generic;

namespace Project.Save
{
    public class TabSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Tab tab = (Tab)obj;
            info.AddValue("Title", tab.Title);
            info.AddValue("ID", tab.ID);
            info.AddValue("Color", tab.Color);
            info.AddValue("Tasks", tab.Tasks);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Tab tab = (Tab)obj;
            tab.Title = (string)info.GetValue("Title", typeof(string));
            tab.ID = (int)info.GetValue("ID", typeof(int));
            tab.Color = (Color32)info.GetValue("Color", typeof(Color32));
            tab.Tasks = (List<Task>)info.GetValue("Tasks", typeof(List<Task>));
            obj = tab;
            return obj;
        }
    }
    public class Color32SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Color32 c = (Color32)obj;
            info.AddValue("r", c.r);
            info.AddValue("g", c.g);
            info.AddValue("b", c.b);
            info.AddValue("a", c.a);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Color32 c = (Color32)obj;
            c.r = (byte)info.GetValue("r", typeof(byte));
            c.g = (byte)info.GetValue("g", typeof(byte));
            c.b = (byte)info.GetValue("b", typeof(byte));
            c.a = (byte)info.GetValue("a", typeof(byte));
            obj = c;
            return obj;
        }
    }


}