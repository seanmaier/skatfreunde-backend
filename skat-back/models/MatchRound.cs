using System.ComponentModel.DataAnnotations;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.models;

public class MatchRound : BaseEntity
{
    [Key] public int Id { get; set; }

    [Required] [MaxLength(MaxIdLength)] public int MatchSessionId { get; set; }

    [Required] public required MatchSession MatchSession { get; set; } = null!;

    public int RoundNumber { get; set; }
}