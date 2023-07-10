using Furnituremarket.Domain.Enum;

namespace Furnituremarket.Domain.Response.Interfaces
{
    public interface IBaseResponse<T>
    {
        public string Description { get; }
        public StatusCode CodeStatus { get; }
        T Data { get; }
    }
}
