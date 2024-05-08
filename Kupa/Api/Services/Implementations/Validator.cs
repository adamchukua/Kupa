using Kupa.Api.Services.Interfaces;

namespace Kupa.Api.Services.Implementations
{
    public class Validator : IValidator
    {
        public void AuthorizedAction(int createdByUserId, int currentUserId, string postMessage)
        {
            AuthorizedUser(currentUserId);

            if (createdByUserId != currentUserId)
            {
                throw new UnauthorizedAccessException($"Access denied: {postMessage}");
            }
        }

        public void AuthorizedOrAdminAction(int createdByUserId, int currentUserId, string currentUserRole, string postMessage)
        {
            AuthorizedUser(currentUserId);

            if (createdByUserId != currentUserId && currentUserRole != "Admin")
            {
                throw new UnauthorizedAccessException($"Access denied: {postMessage}");
            }
        }

        public void AuthorizedUser(int? userId)
        {
            if (userId == 0 || userId == null)
            {
                throw new UnauthorizedAccessException("User isn't authorized");
            }
        }

        public void ObjectNull(object obj, string name, string? message = null)
        {
            if (obj == null)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    throw new ArgumentException(message);
                }

                throw new ArgumentException($"{name} is null");
            }
        }

        public void PositiveInt(int number, string name)
        {
            if (number <= 0)
            {
                throw new ArgumentException($"{name} must be greater than zero");
            }
        }

        public void StringNullOrEmpty(string str, string name)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException($"{name} is null or empty");
            }
        }
    }
}
