namespace TotCS
{
    public struct TotItem
    {
        public string Name { get; set; }
        public string Data { get; set; }

        public TotItem(string name = "", string data = "")
        {
            Name = name;
            Data = data;
        }
    }
}
