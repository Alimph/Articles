using Articles.Protos;
using Grpc.Core;

namespace Articles.Services
{
    public class ArticleApi : ArticleService.ArticleServiceBase
    {
        private readonly ArticleList _articles = new ArticleList();
        public override Task<DefaultResponse> Create(Article request, ServerCallContext context)
        {
            _articles.Items.Add(request);
            return Task.FromResult(new DefaultResponse
            {
                Message = "job successful."
            });
        }

        public override async Task GetAll(GetArticleCount request, IServerStreamWriter<Article> responseStream, ServerCallContext context)
        {
            var i = 0;
            while (!context.CancellationToken.IsCancellationRequested && i < 50)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(_articles.Items[i]);
                i++;
            }
        }
    }
}
