using CAM.Core.SharedKernel;

namespace CAM.Core.Interfaces
{
    public interface IPaginatedListMapper
    {
        PaginatedList<T> MapToViewModelList<T, U>(PaginatedList<U> orig);
    }
}