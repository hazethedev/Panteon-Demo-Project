using CrashKonijn.Goap.Interfaces;

namespace DemoProject.AI.Data
{
    public class CommonData : IActionData
    {
        public ITarget Target { get; set; }
        public float Timer { get; set; }
    }
}