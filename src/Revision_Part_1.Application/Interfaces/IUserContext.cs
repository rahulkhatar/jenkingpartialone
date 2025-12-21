using Revision_Part_1.Application.User;

namespace Revision_Part_1.Application.Interfaces;

public interface IUserContext
{
    Current? GetCurrent();
}
