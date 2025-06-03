namespace skat_back.Lib;

/// <summary>
///     Defines a base service interface for CRUD operations.
/// </summary>
/// <typeparam name="TResponseDto">The type of the response DTO.</typeparam>
/// <typeparam name="TCreateDto">The type of the DTO used for creating entities.</typeparam>
/// <typeparam name="TUpdateDto">The type of the DTO used for updating entities.</typeparam>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public interface IService<TResponseDto, in TCreateDto, in TUpdateDto, in TId>
    where TResponseDto : class
    where TCreateDto : class
    where TUpdateDto : class
    where TId : struct
{
    Task<PagedResult<TResponseDto>> GetAllAsync(PaginationParameters parameters);
    Task<TResponseDto?> GetByIdAsync(TId id);
    Task<TResponseDto> CreateAsync(TCreateDto dto);
    Task<bool> UpdateAsync(TId id, TUpdateDto dto);
    Task<bool> DeleteAsync(TId id);
}