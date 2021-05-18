using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebApplication1.EfStuff.Model.Television
{
    public enum Occupation
    {
        TvAdmin = 0,
        Director = 1,
        Producer = 2,
        Cameraman = 3,
        [EnumMember(Value = "Casting Director")]
        CastingDirector=4
    }
}
