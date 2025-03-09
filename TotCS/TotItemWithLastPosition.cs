namespace TotCS
{
    public struct TotItemWithLastPosition
    {
        public string Name { get; set; }
        public string Data { get; set; }
        public long LastPosition { get; set; }

        public TotItemWithLastPosition(string name = "", string data = "", long lastPosition = -1)
        {
            Name = name;
            Data = data;
            LastPosition = lastPosition;
        }
    }
}
