namespace Kupa.Api.Services.Interfaces
{
    public interface IValidator
    {
        void PositiveInt(int number, string name);

        void ObjectNull(object obj, string name, string? message = null);

        void StringNullOrEmpty(string str, string name);

        void AuthorizedAction(int createdByUserId, int currentUserId, string postMessage);

        void AuthorizedOrAdminAction(int createdByUserId, int currentUserId, string currentUserRole, string postMessage);

        void AuthorizedUser(int? userId);
    }
}
