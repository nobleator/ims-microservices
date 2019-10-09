using System.ComponentModel.DataAnnotations;

namespace web.Enums
{
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-2.2#the-select-tag-helper
    public enum TransactionTypeEnum
    {
        [Display(Name = "Purchase")]
        Purchase,
        Sale
    }
}