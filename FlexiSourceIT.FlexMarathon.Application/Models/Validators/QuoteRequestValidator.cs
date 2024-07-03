using FluentValidation;
using SMEAppHouse.Ladon.Domain.Constants;
using SMEAppHouse.Core.CodeKits;
using SMEAppHouse.Ladon.Application.Models.Data;

namespace SMEAppHouse.Ladon.Application.Models.Validators;

public class QuoteRequestValidator : AbstractValidator<QuoteRequestModel>
{
    public QuoteRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First Name is required")
            .Length(1, FieldLengths.QuoteRequest.FirstName)
            .WithMessage($"First Name must be less than {FieldLengths.QuoteRequest.FirstName} characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last Name is required")
            .Length(1, FieldLengths.QuoteRequest.LastName)
            .WithMessage($"Last Name must be less than {FieldLengths.QuoteRequest.LastName} characters");

        RuleFor(x => x.CompanyName)
            .MaximumLength(FieldLengths.QuoteRequest.CompanyName)
            .WithMessage($"Company Name must be less than {FieldLengths.QuoteRequest.CompanyName} characters");

        RuleFor(x => x.EmailAddress)
            .NotEmpty().WithMessage("Email address is required")
            .EmailAddress().WithMessage("A valid Email address is required")
            .MaximumLength(FieldLengths.QuoteRequest.EmailAddress)
            .WithMessage($"Email address must be less than {FieldLengths.QuoteRequest.EmailAddress} characters");

        RuleFor(x => x.PhoneNo)
            .Empty().When(x => string.IsNullOrWhiteSpace(x.PhoneNo))
            .WithMessage("Phone number is required.")
            .Matches(@"^\+?[0-9\s()-]{1,20}$")
            .WithMessage($"Phone number is not valid or exceeds maximum length of {FieldLengths.QuoteRequest.PhoneNumber} characters");

        RuleFor(x => x.ContactPreference)
            .IsInEnum()
            .WithMessage("Contact preference selection is required")
            .Custom((contactPreference, context) =>
            {
                var quoteRequest = context.InstanceToValidate;
                if ((contactPreference == Domain.Constants.Rules.ContactMethodPreferencesEnum.Phone ||
                    contactPreference == Domain.Constants.Rules.ContactMethodPreferencesEnum.Both)
                    && string.IsNullOrEmpty(quoteRequest.PhoneNo))
                {
                    context.AddFailure("Phone Number is required when contact preference is set to Phone or Both.");
                }
            });

        RuleFor(x => x.Address)
            .MaximumLength(FieldLengths.QuoteRequest.AddressLine1)
            .WithMessage($"Address must be less than {FieldLengths.QuoteRequest.AddressLine1} characters");

        RuleFor(x => x.City)
            .MaximumLength(FieldLengths.QuoteRequest.City)
            .WithMessage($"City must be less than {FieldLengths.QuoteRequest.City} characters");

        RuleFor(x => x.ZipCode)
            .MaximumLength(FieldLengths.QuoteRequest.ZipCode)
            .WithMessage($"Zip Code must be less than {FieldLengths.QuoteRequest.ZipCode} characters");

        RuleFor(x => x.StateOrRegion)
            .MaximumLength(FieldLengths.QuoteRequest.StateOrRegion)
            .WithMessage($"State or Region must be less than {FieldLengths.QuoteRequest.StateOrRegion} characters");

        RuleFor(x => x.CountryCode)
            .MaximumLength(FieldLengths.QuoteRequest.CountryCode)
            .WithMessage($"Country code must be less than {FieldLengths.QuoteRequest.CountryCode} characters");

        RuleFor(x => x.SiteProjectName)
            .NotEmpty().WithMessage("Project name is required")
            .MaximumLength(FieldLengths.QuoteRequest.SiteProjectName)
            .WithMessage($"Project name must be less than {FieldLengths.QuoteRequest.SiteProjectName} characters");

        RuleFor(x => x.ServiceCode)
            .MaximumLength(FieldLengths.QuoteRequest.ProjectServiceTypeCode)
            .WithMessage($"Service code must be less than {FieldLengths.QuoteRequest.ProjectServiceTypeCode} characters");

        RuleFor(x => x.SiteAddress)
            .MaximumLength(FieldLengths.QuoteRequest.SiteAddressLine1)
            .WithMessage($"Site Address must be less than {FieldLengths.QuoteRequest.SiteAddressLine1} characters")
            .Custom((siteAddress, context) =>
            {
                var quoteRequest = context.InstanceToValidate;
                if (string.IsNullOrEmpty(siteAddress) && !quoteRequest.SiteAddressSameAsResidential)
                {
                    context.AddFailure("Site address is required when you have not opted here to be the same as the address above.");
                }
            });

        RuleFor(x => x.SiteCity)
            .MaximumLength(FieldLengths.QuoteRequest.SiteCity)
            .WithMessage($"Site City must be less than {FieldLengths.QuoteRequest.SiteCity} characters");

        RuleFor(x => x.SiteZipCode)
            .MaximumLength(FieldLengths.QuoteRequest.SiteZipCode)
            .WithMessage($"Site Zip Code must be less than {FieldLengths.QuoteRequest.SiteZipCode} characters");

        RuleFor(x => x.SiteStateOrRegion)
            .MaximumLength(FieldLengths.QuoteRequest.SiteStateOrRegion)
            .WithMessage($"Site Region must be less than {FieldLengths.QuoteRequest.SiteStateOrRegion} characters");

        RuleFor(x => x.SiteCountryCode)
            .MaximumLength(FieldLengths.QuoteRequest.SiteCountry)
            .WithMessage($"Site Country must be less than {FieldLengths.QuoteRequest.SiteCountry} characters");

        RuleFor(x => x.DesiredStartDate)
            .NotEmpty().When(x => x.DesiredEndDate.HasValue)
            .WithMessage("Desired Start Date must be provided if Desired End Date is specified");

        RuleFor(x => x.DesiredEndDate)
            .GreaterThanOrEqualTo(x => x.DesiredStartDate)
            .WithMessage("End date cannot be less than start date");

        RuleFor(x => x.DurationMonths)
            .Custom((durationMonths, context) =>
            {
                var quoteRequest = context.InstanceToValidate;
                if (!quoteRequest.DesiredStartDate.HasValue &&
                    !quoteRequest.DesiredEndDate.HasValue &&
                    (quoteRequest.DurationMonths.IsNullOrZero()) &&
                    quoteRequest.DurationDays == null &&
                    quoteRequest.DurationHours == null)
                {
                    context.AddFailure("While desired start and end dates are not supplied, at least please supply the number of months, days, or hours desirable for you to complete this project.");
                }
            });

        RuleFor(x => x.SpecialRequirementsOrFeatures)
            .NotEmpty().WithMessage("Your requirement or feature descriptions are required")
            .MaximumLength(FieldLengths.QuoteRequest.SpecialRequirementsOrFeatures)
            .WithMessage($"Requirements or Features must be less than {FieldLengths.QuoteRequest.SpecialRequirementsOrFeatures} characters");

        RuleFor(x => x.MaximumBudgetAmount)
            .GreaterThanOrEqualTo(x => x.MinimumBudgetAmount)
            .WithMessage("Maximum Budget Amount cannot be less than the Minimum Budget Amount");

        RuleFor(x => x.AdditionalCommentsOrQuestions)
            .MaximumLength(FieldLengths.QuoteRequest.AdditionalCommentsOrQuestions)
            .WithMessage($"Additional comments or questions must be less than {FieldLengths.QuoteRequest.AdditionalCommentsOrQuestions} characters");

        RuleFor(x => x.ReferralSource)
            .IsInEnum()
            .WithMessage("Referral source selection is required");

        // Custom validation rule for DesiredStartDate, DesiredEndDate, DurationMonths, DurationDays, DurationHours
        //RuleFor(x => x)
        //    .Custom((quoteRequest, context) =>
        //    {
        //        if (!quoteRequest.DesiredStartDate.HasValue &&
        //            !quoteRequest.DesiredEndDate.HasValue &&
        //            !quoteRequest.DurationMonths.HasValue &&
        //            !quoteRequest.DurationDays.HasValue &&
        //            !quoteRequest.DurationHours.HasValue)
        //        {
        //            context.AddFailure("While desired start and end dates are not supplied, at least supply the number of months or days desirable for you to complete this project.");
        //        }
        //    });
    }
}
