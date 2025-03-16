namespace Api.GRRInnovations.TodoManager.Domain.ValueObjects
{
    public class UserOptions
    {
        public IReadOnlyList<Guid> FilterUsers { get; }
        public IReadOnlyList<string> FilterLogins { get; }
        public bool IncludeUserTasks { get; }
        public bool IncludeUserDetail { get; }

        private UserOptions(
            IEnumerable<Guid> filterUsers = null,
            IEnumerable<string> filterLogins = null,
            bool includeUserTasks = false,
            bool includeUserDetail = false)
        {
            FilterUsers = filterUsers?.ToList() ?? new List<Guid>();
            FilterLogins = filterLogins?.ToList() ?? new List<string>();
            IncludeUserTasks = includeUserTasks;
            IncludeUserDetail = includeUserDetail;
        }

        public static Builder Create() => new Builder();

        public class Builder
        {
            private List<Guid> _filterUsers = new();
            private List<string> _filterLogins = new();
            private bool _includeUserTasks;
            private bool _includeUserDetail;

            public Builder WithUsers(IEnumerable<Guid> users)
            {
                _filterUsers = users?.ToList() ?? new List<Guid>();
                return this;
            }

            public Builder WithLogins(IEnumerable<string> logins)
            {
                _filterLogins = logins?.ToList() ?? new List<string>();
                return this;
            }

            public Builder WithTasks(bool include)
            {
                _includeUserTasks = include;
                return this;
            }

            public Builder WithDetails(bool include)
            {
                _includeUserDetail = include;
                return this;
            }

            public UserOptions Build()
            {
                return new UserOptions(_filterUsers, _filterLogins, _includeUserTasks, _includeUserDetail);
            }
        }
    }
}
