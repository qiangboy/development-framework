using BookStore.Email;
using Volo.Abp.Settings;

namespace BookStore
{
    public class EmailSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            var smtpPassword = context.GetOrNull(EmailSettings.PasswordKey);
            if (smtpPassword != null)
            {
                smtpPassword.IsEncrypted = false;
            }
        }
    }
}
