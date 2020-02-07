using SQLite;

namespace XamarinToDoList
{
    public class User
    {
        [PrimaryKey, Column("Email")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
