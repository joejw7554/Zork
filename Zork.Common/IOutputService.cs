namespace Zork.Common
{
    public interface IOutputService
    {
        void Write(string message);
        void Write(object obj);
        void WriteLine(object obj);
        void WriteLine(string messsage);
    }
}
