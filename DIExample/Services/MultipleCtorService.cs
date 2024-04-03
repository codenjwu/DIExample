namespace DIExample.Services
{
    public class MultipleCtorService : IMultipleCtorService
    {
        public MultipleCtorService() { Param1 = "Default"; Param2 = "Default"; }
        public MultipleCtorService(string par1) : this() { Param1 = par1; Param2 = par1; }
        public MultipleCtorService(string par1, string par2) : this(par1) { Param2 = par2; }

        public string Param1 { get; set; }
        public string Param2 { get; set; }

        public string Value()
        {
            return $"{this.GetType().Name}, param1: {Param1}, param2: {Param2}";
        }
    }
    public interface IMultipleCtorService
    {
        string Value();
        string Param1 { get; set; }
        string Param2 { get; set; }
    }
}
