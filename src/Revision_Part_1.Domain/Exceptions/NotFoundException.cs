namespace Revision_Part_1.Domain.Exceptions;

public class NotFoundException(string resourceType, string rsourceIdentifier) : 
    Exception($"{resourceType} with id: {rsourceIdentifier} doesn't exists.....")
{

}
