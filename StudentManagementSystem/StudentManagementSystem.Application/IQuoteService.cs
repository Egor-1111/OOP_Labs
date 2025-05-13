using System.Threading.Tasks;
using StudentManagementSystem.Application.DTOs;

namespace StudentManagementSystem.Application
{
    public interface IQuoteService
    {
        Task<QuoteDto> GetMotivationalQuote();
    }
}