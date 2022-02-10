using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.EmailHandler.Configuration;

namespace LibraryManagement.EmailHandler.Services.Abstraction
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
