using Volo.Abp.TextTemplating;

namespace BookStore.Volo.Abp.Emailing
{
    public class EmailingTemplateDefinitionProvider : TemplateDefinitionProvider
    {
        public override void Define(ITemplateDefinitionContext context)
        {
            context.Add(
                new TemplateDefinition(EmailConsts.DefaultDefinitionName) //template name: "Hello"
                    .WithVirtualFilePath(
                        "/Volo/Abp/Emailing/Templates/Layout.tpl", //template content path
                        isInlineLocalized: true
                    )
            );
        }
    }

}
