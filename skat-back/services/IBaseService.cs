namespace skat_back.services;

public interface IBaseService<TResponseDto, in TCreateDto, in TUpdateDto, in TId>
    where TResponseDto : class
    where TCreateDto : class
    where TUpdateDto : class
    where TId : struct
{
    Task<IEnumerable<TResponseDto>> GetAllAsync();
    Task<TResponseDto?> GetByIdAsync(TId id);
    Task<TResponseDto> CreateAsync(TCreateDto entity);
    Task<bool> UpdateAsync(TId id, TUpdateDto entity);
    Task<bool> DeleteAsync(TId id);
}