using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

//? To Fix this GodDamn Error : The INSERT statement conflicted with the FOREIGN KEY SAME TABLE constraint "". The conflict occurred in database "", table "", column 'Id'.
//! Just do this in table setting database  just go to relationships and set this Enforce Foreign key Constraint to No Just this ez Pz Milad

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment : EntityBase
    {
        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Message { get; private set; }

        public bool IsConfirmed { get; private set; }

        public bool IsCanceled { get; private set; }

        public int OwnerRecordId { get; private set; }

        public int Type { get; private set; }

        public int ParentId { get; private set; }

        public Comment Parent { get; private set; }

        public Comment(string name, string email, string message, int ownerRecordId, int type , int parentId)
        {
            Name = name;
            Email = email;
            Message = message;
            OwnerRecordId = ownerRecordId;
            Type = type;
            ParentId = parentId;
        }

        public void Confirm()
        {
            IsConfirmed = true;
            IsCanceled = false;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }
    }
}
