using UnityEngine;

namespace Project.Logic
{
    public struct Tab
    {
        public string Title { get; set; }
        public int ID { get; set; } 
        public Color32 Color { get; set; }

        public void SetTitle(string title) => Title = title;
        public void SetID(int id) => ID = id;
        public void SetColor(Color32 color) => Color = color;
    }
}