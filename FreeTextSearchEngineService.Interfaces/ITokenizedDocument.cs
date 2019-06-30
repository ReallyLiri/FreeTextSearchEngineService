using System.Collections.Generic;

namespace FreeTextSearchEngineService.Interfaces
{
    public interface ITokenizedDocument
    {
        string Document { get; }
        ICollection<string> Tokens { get; }
    }
}
