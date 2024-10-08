using Articles.Protos;
using Bogus;

namespace Articles.Data
{
    public class ArticleDto
    {
        private static List<Article> _items;
        public List<Article> Items => _items;
        public void Add(Article article)
        {
            _items.Add(article);    
        }
        public ArticleDto()
        {
            if (_items is null)
            {
                _items = new List<Article>();   
                var faker = new Faker("fa");
                for (int i = 0; i < 30; i++)
                {
                      Add(
                        new Article
                        {
                            Author = faker.Name.FullName(),
                            DateOfRelease = faker.DateTimeReference.ToString(),
                            Text = faker.Lorem.Text(),
                            Title = faker.Name.JobTitle(),
                            Type = ArticleType.Text,
                        });
                    
                }
            }
        }

    }
}
