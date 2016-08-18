
using System.ComponentModel.DataAnnotations;
using Domain.EStore.Repositories;

namespace Domain.EStore
{

    public partial class Branch : IEntity { }
    public partial class Brand : IEntity { }
    public partial class Category : IEntity { }
    public partial class City : IEntity { }
    public partial class Enquiry : IEntity { }
    public partial class GetWay : IEntity { }
    public partial class Governate : IEntity { }
    public partial class LestCategory : IEntity { }
    public partial class HomeDetail : IEntity { }
    public partial class MainCategory : IEntity { }
    public partial class Privilege : IEntity { }
    public partial class PrivilegeAction : IEntity { }
    public partial class PrivilegeCategory : IEntity { }
    public partial class Product : IEntity { }
    public partial class ProductShow : IEntity { }
    public partial class ProductImage : IEntity { }
    public partial class Rule : IEntity { }
    public partial class RulePrivilege : IEntity { }
    public partial class SliderImage : IEntity { }
    public partial class Speciality : IEntity { }
    public partial class SysEmail : IEntity { }
    public partial class SysNew : IEntity { }
    public partial class User : IEntity { }
    public partial class UserOperation : IEntity { }
    public partial class UserOperationType : IEntity { }
    public partial class ViewControl : IEntity { }

    [MetadataType(typeof(CustomerMetaData))]
    public partial class Customer : IEntity
    {

        public class CustomerMetaData
        {


            [Required]
            public string UserName { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            public string Email { get; set; }
        }
    }

    public partial class UserProfile : IEntity { public int Id { get { return UserId; } } }

    public partial class delivery : IEntity { }

    public partial class Cart : IEntity { }

    public partial class CartDetail : IEntity { }
    public partial class Rating : IEntity { }

    public partial class FAQ : IEntity
    {
        public int Id
        {
            get { return FAQId; }
        }
    }
}
