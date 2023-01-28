namespace PiecykPolHurt.Model.Queries
{
    public class GeneratorQuery
    {
        public int Reportid { get; set; }
        public int? MaxRows { get; set; }
        public IEnumerable<GeneratorParamValue> ParamsValues { get; set; }
    }
}
