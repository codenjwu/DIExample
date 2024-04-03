namespace DIExample.Services
{
    public class ParentSerivce : IParentSerivce
    {
        public string Money()
        {
            return $"{this.GetType().Name}, {nameof(Money)}";
        }
    }
    public class ChildService : IChildSerivce,IParentSerivce,ISiblingSerivce
    {
        public string Money()
        {
            return $"{this.GetType().Name}, {nameof(Money)}";
        }

        public string MoreToy()
        {
            return $"{this.GetType().Name}, {nameof(MoreToy)}";
        }

        public string Toy()
        {
            return $"{this.GetType().Name}, {nameof(Toy)}";
        }
         
    }
    public class SiblingSerivce : ISiblingSerivce,IParentSerivce
    {
        public string Money()
        {
            return $"{this.GetType().Name}, {nameof(Money)}";
        }

        public string MoreToy()
        {
            return $"{this.GetType().Name}, {nameof(MoreToy)}";
        }
    }
    public interface IParentSerivce
    {
        string Money();
    }
    public interface IChildSerivce
    {
        string Toy();
    }
    public interface ISiblingSerivce
    {
        string MoreToy();
    }
}
