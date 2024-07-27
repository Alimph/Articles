using Articles.Services;

namespace Articles
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //register grpc service
            builder.Services.AddGrpc(op => op.EnableDetailedErrors = true);

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            app.UseAuthorization();

            //grpc route service
            app.MapGrpcService<ArticleApi>();
            
            app.MapControllers();

            app.Run();
        }
    }
}
