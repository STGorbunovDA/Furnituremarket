using Furnituremarket.Domain.Enum;
using Furnituremarket.Domain.Response.Interfaces;

namespace Furnituremarket.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }
        public StatusCode CodeStatus { get; set; }
        public T Data { get; set; }
    }
}
