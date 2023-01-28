namespace PiecykPolHurt.Model.Commands
{
    public class CreateReportDefinitionCommand
    {
        public string Title { get; set; }
        public string Group { get; set; }
        public string Description { get; set; }
        public int MaxRow { get; set; }
        public string XmlDefinition { get; set; }
    }
}
