﻿using System.Threading.Tasks;

namespace KeyForgeGameTracker.Services
{
    public interface IEmailService
    {
        Task NewAccountAsync(string email, string callbackUrl);

        Task ResetPasswordAsync(string email, string callbackUrl);

        Task ConfirmEmailAsync(string email, string callbackUrl);

        Task ConfirmNewEmailAsync(string email, string callbackUrl);
    }
}
