namespace Application.Common.Queues
{
    public interface IEmailQueue
    {
        void Send(string email);
    }
}