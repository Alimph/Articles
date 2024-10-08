using Grpc.Core;
using Grpc.Net.Client;
using Weblog.Protos;

namespace Weblog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapGet("/StreamEndpoint", () =>
            {
                async IAsyncEnumerable<Article> FetchData()
                {
                    using var channel = GrpcChannel.ForAddress("https://localhost:7013");

                    var client = new ArticleService.ArticleServiceClient(channel);

                    using var call = client.GetAll(new GetArticleCount { Count = 3 });

                    //var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                    //or none
                    await foreach (var article in call.ResponseStream.ReadAllAsync(CancellationToken.None))
                    {
                        yield return (article);
                    }
                }
                return FetchData();
            });

            app.Run();
        }
    }
}
