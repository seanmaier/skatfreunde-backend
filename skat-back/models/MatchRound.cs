using System.ComponentModel.DataAnnotations;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.models;

public class MatchRound : BaseEntity
{
    [Key] public int Id { get; set; }

    [MaxLength(MaxCharLength)] public string RoundNumber { get; set; }

    /*--------------------Navigation  Properties--------------------*/

    [Required] public int MatchSessionId { get; set; }
    public MatchSession MatchSession { get; set; } = null!;

    public ICollection<PlayerRoundStats> PlayerRoundResults { get; set; } = null!;
}