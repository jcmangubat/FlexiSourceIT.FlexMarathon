using AutoFilterer.Attributes;
using AutoFilterer.Types;
using SMEAppHouse.Core.Patterns.EF.ModelComposites.Interfaces;

namespace FlexiSourceIT.FlexMarathon.Application.Models.Base;

[GenerateAutoFilter]
public class KeyedModel : AutoFilterer.Types.FilterBase, IEntityKeyedArchivable<Guid>
{
    [CompareTo]
    public Guid Id { get; set; }

    [CompareTo]
    public bool? IsActive { get; set; } = true;

    [CompareTo]
    public DateTime DateCreated { get; set; } = DateTime.Now;

    [CompareTo]

    public DateTime? DateModified { get; set; } = DateTime.Now;

    [CompareTo]
    public bool? IsArchived { get; set; }

    [CompareTo]
    public DateTime? DateArchived { get; set; }

    [CompareTo]
    public string? ReasonArchived { get; set; }
}
