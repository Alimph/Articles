using Articles.Protos;
using Bogus;

namespace Articles.Data
{
    public class ArticleList
    {
        public ArticleList()
        {
            if (Articles is null)
            {
                var faker = new Faker("fa");
                for (int i = 0; i < 10; i++)
                {
                    Articles = new List<Article>
                    {
                        new Article
                        {
                           Author = faker.Name.FullName(),
                           DateOfRelease = faker.Date.Locale,
                           Text = faker.Lorem.Text(),
                           Title = faker.Name.JobTitle(),
                           Type = ArticleType.Text,
                        }
                    };
                }
            }
        }
        public static List<Article> Articles { get; set; }

    }
}
