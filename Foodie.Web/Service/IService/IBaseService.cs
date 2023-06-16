using Foodie.Web.Model;
using Foodie.Web.Models;

namespace Foodie.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto>? SendAsync(RequestDto request);
    }
}
