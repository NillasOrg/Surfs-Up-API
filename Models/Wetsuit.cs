using Azure.Core;

namespace Surfs_Up_API.Models;

public class Wetsuit : ICartItem
{
    public int Id {get; set;}
    public double Price {get; set;} = 110;
    public int Size {get; set;}
    public Gender Gender {get; set;}
}

public enum Gender
{
    Male, Female
}