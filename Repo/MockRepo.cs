using Art_WebAPI.Models;
using System.Reflection.Metadata.Ecma335;

namespace Art_WebAPI.Repo
{
    // interface for data access
    public interface IArtRepo
    {
        IEnumerable<Art> GetAll();
        Art? GetById(int? id);
        void Add(Art article);
        void Update(Art article);
        void Delete(int id);
    }
    // mock repository class for data access
    public class MockArtRepo : IArtRepo
    {
        private readonly List<Art> _articles = new();

        public IEnumerable<Art> GetAll() => _articles;

        public Art? GetById(int? id)
        {
            if(id == null)
                return null;
            foreach (var a in _articles)
                if (a.Id == id)
                    return a;

            return null;
        }

        public void Add(Art article) => _articles.Add(article);

        public void Update(Art article)
        {
            var existingArticle = GetById(article.Id);
            if (existingArticle != null)
            {
                existingArticle.Title = article.Title;
                existingArticle.Content = article.Content;
                existingArticle.PublishedDate = article.PublishedDate;
            }
        }

        public void Delete(int id)
        {
            var article = GetById(id);
            if (article != null)
            {
                _articles.Remove(article);
            }
        }
    }
}
