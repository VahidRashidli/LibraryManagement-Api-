using System;

namespace DomainModels.Models.Base
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime StartDate { get; set; }
        DateTime DeletedDate { get; set; }
        bool IsDeleted { get; set; }
    }
}
