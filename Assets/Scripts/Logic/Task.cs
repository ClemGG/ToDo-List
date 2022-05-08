
namespace Project.Logic
{
    public struct Task
    {
        public string Text { get; set; }
        public int ID { get; set; }
        public bool Done { get; set; }


        public void SetText(string text) => Text = text;
        public void SetID(int id) => ID = id;
        public void SetDone(bool done) => Done = done;
    }

}