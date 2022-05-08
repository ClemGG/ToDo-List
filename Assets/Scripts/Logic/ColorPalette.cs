using UnityEngine;

namespace Project.Logic
{
    [System.Serializable]
    public class ColorPalette
    {
        [field: SerializeField] Color32[] Palette { get; set; } = new Color32[16];

        public Color this[int index]
        {
            get
            {
                return Palette[index];
            }
            set
            {
                Palette[index] = value;
            }
        }

        public Color32 GetRandomColor()
        {
            return Palette[Random.Range(0, Palette.Length)];
        }
    }
}