namespace Dessins.Events
{
    public class ChangeColor : DrawingEvent
    {
        public override string Type { get { return "ChangeColour"; } }
        public string Color { get; set; }

        public ChangeColor(string name)
        {
            Color = name;
        }
    }
}
