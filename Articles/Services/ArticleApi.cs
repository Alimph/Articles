using Articles.Data;
using Articles.Protos;
using Grpc.Core;

namespace Articles.Services
{
    public class ArticleApi : ArticleService.ArticleServiceBase
    {
        private readonly ArticleDto _articles = new ArticleDto();
        public override Task<DefaultResponse> Create(Article request, ServerCallContext context)
        {
            _articles.Add(request);
            return Task.FromResult(new DefaultResponse
            {
                Message = "job successful."
            });
        }

        public override async Task GetAll(GetArticleCount request, IServerStreamWriter<Article> responseStream, ServerCallContext context)
        {
            var i = 0;
            var count = request.Count > 0 ? request.Count : _articles.Items.Count;
            while (!context.CancellationToken.IsCancellationRequested && i < count)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(_articles.Items[i]);
                i++;
            }
        }

        public override Task<Article> SayHello(GetArticleCount request, ServerCallContext context)
        {
            return Task.FromResult(new Article { Author = "ALi" });
        }
    }
}
