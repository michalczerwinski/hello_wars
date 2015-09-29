namespace Common.Models
{
    public class RoundPartialHistory
    {
        public string Caption { get; set; }
        public object BoardState { get; set; }

        public override string ToString()
        {
            return Caption;
        }
    }
}
