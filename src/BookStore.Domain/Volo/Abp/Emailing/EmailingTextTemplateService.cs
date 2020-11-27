using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.TextTemplating;

namespace BookStore.Volo.Abp.Emailing
{
    public class EmailingTextTemplateService : ITransientDependency
    {
        private readonly ITemplateRenderer _templateRenderer;

        public EmailingTextTemplateService(ITemplateRenderer templateRenderer)
        {
            _templateRenderer = templateRenderer;
        }

        public async Task<string> RunAsync()
        {
            var result = await _templateRenderer.RenderAsync(
                EmailConsts.DefaultDefinitionName, //the template name
                new 
                {
                    Content = "阿里路亚"
                }
            );

            return result;
        }
    }
}
