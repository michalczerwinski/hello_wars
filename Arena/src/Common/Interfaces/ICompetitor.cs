using System;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface ICompetitor
    {
        Guid Id { get; }
        string Name { get; set; }
        string Description { get; set; }
        string AvatarUrl { get; set; }
        string Url { get; set; }
        bool IsVerified { get; }

        Task VerifyAsync(string gameType);
        // TODO: Verify Method
    }
}
