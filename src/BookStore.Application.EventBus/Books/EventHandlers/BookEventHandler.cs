using System.Threading.Tasks;
using BookStore.Volo.Abp.Emailing;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MailKit;

namespace BookStore.Application.EventBus.Books.EventHandlers
{
    public class BookEventHandler : IDistributedEventHandler<BookEventData>, ITransientDependency
    {
        private readonly IMailKitSmtpEmailSender _mailKitSmtpEmailSender;
        private readonly EmailingTextTemplateService _emailingTextTemplateService;

        public BookEventHandler(
            IMailKitSmtpEmailSender mailKitSmtpEmailSender,
            EmailingTextTemplateService emailingTextTemplateService)
        {
            _mailKitSmtpEmailSender = mailKitSmtpEmailSender;
            _emailingTextTemplateService = emailingTextTemplateService;
        }

        public async Task HandleEventAsync(BookEventData eventData)
        {
            // 发送邮件
            await _mailKitSmtpEmailSender.SendAsync(
                "2314862535@qq.com",     // target email address
                "我爱你",         // subject
                 await _emailingTextTemplateService.RunAsync()
            );
        }
    }
}
