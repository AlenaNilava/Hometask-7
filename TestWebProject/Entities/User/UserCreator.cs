namespace TestWebProject.Entities.User
{
    public abstract class UserCreator
    {
        public UserCreator() // constructor
        {

        }

        abstract public User Create(string login, string password);
        
    }
}
