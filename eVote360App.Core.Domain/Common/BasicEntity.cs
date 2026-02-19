namespace eVote360App.Core.Domain.Common
{
    public class BasicEntity<TKey>
    {
        public required TKey Id { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
