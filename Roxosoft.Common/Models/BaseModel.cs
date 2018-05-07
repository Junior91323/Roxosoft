namespace Roxosoft.Common.Models
{
    using System;

    public class BaseModel
    {
        public Guid? CreateUserUid { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid? UpdateUserUid { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool IsActive { get; set; }
    }
}
