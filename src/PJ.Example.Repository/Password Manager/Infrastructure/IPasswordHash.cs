namespace PJ.Example.Repository.Password_Manager.Infrastructure
{
    internal interface IPasswordHash
    {
        string Encode(string password);

        bool Compare(string password, string hashedPassword);
    }
}