using WishCreator.Models;

namespace WishCreator.Services
{
    public interface ISearchService
    {
        SearchResult Search(string searchQuery);
    }
}