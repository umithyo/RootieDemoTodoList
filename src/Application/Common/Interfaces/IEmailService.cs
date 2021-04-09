using System;
using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IEmailService
    {
        void Send(Action<EmailOptions> options);
    }
}