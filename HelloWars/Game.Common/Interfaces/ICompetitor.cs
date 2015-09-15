using System;

namespace Common.Interfaces
{
    public interface ICompetitor
    {
        Guid Id { get; }
        string Name { get; set; }
        string AvatarUrl { get; set; }
        string Url { get; set; }

        // TODO: Verify Method
    }
}
