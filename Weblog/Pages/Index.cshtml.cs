using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Weblog.Protos;

namespace Weblog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public List<Article> Articles { get; set; } = new();

        public async Task OnGet()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7013");

            var client = new ArticleService.ArticleServiceClient(channel);

            using var call = client.GetAll(new GetArticleCount { Count = 25 });

            //var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            //or none
            await foreach (var article in call.ResponseStream.ReadAllAsync(CancellationToken.None))
            {
                Articles.Add(article);
            }
        }




    }
}
