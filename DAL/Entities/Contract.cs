using DAL.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public partial class Contract
{
    [Required]
    public int ContractId { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Must be between 5 and 100 characters long")]
    [NoSpecialChars]
    public string ContractTitle { get; set; } = null!;
    [Required]
    [RegularExpression("^(Apartment|Commercial|Land|House|Villa)$", ErrorMessage = "Must be one of the predefined values")]
    public string PropertyType { get; set; } = null!;
    [Required]
    [NotFutureDate]
    public DateOnly SigningDate { get; set; }
    [Required]
    [AfterSigningDate]
    public DateOnly ExpirationDate { get; set; }
    [Required]
    public int BrokerId { get; set; }
    [Required]
    [Range(1001, double.MaxValue, ErrorMessage = "Value must be greater than 1000")]
    public decimal Value { get; set; }
    public virtual Broker Broker { get; set; } = null!;
}
