namespace AppShared.ViewModels
{
    public class StatisticPageViewModel
    {
        public StatBlock b1 { get; set; }
        public StatBlock b2 { get; set; }
        public StatBlock b3 { get; set; }
        public StatBlock b4 { get; set; }
    }

    public class StatBlock
    {
        public string Title { get; set; }
        public string MainValue { get; set; }
        public string FooterText { get; set; }
        public string FooterValue { get; set; }
        public string Row1 { get; set; }
        public string Row2 { get; set; }
    }
}
