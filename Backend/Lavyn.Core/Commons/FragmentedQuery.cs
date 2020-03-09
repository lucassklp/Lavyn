namespace Lavyn.Core.Commons
{
    public class FragmentedQuery<TFilterDto>
        where TFilterDto: class
    {
        public TFilterDto Filter { get; set; }
        public int Offset { get; set; }
        public int Index { get; set; }
    }
}