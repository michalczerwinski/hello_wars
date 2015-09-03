using System;

namespace Common.Interfaces
{
    public interface ICompetitor
    {
        Guid Id { get; }
        string Name { get; }
        string AvatarUrl { get; }
        string Url { get; }
    }
}
