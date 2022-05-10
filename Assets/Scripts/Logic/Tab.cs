using System.Collections.Generic;
using UnityEngine;

namespace Project.Logic
{
    public class Tab
    {
        public string Title { get; set; }
        public int ID { get; set; } 
        public Color32 Color { get; set; }
        public List<Task> Tasks { get; set; } = new List<Task>();

    }
}