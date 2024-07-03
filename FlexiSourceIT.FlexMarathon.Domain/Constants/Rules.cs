namespace FlexiSourceIT.FlexMarathon.Domain.Constants;

public class Rules
{
    public enum UserRulesEnum
    {
        Admin = 0,
        Manager = 1,
        Editor = 2,
        Moderator = 3,
        Client = 4,
        Visitor = 5
    }

    public enum AddressTypesEnum
    {
        /// <summary>
        /// The residential address where a person lives.
        /// </summary>
        HomeAddress = 1,
        /// <summary>
        /// The address of a person's workplace or place of employment.
        /// </summary>
        WorkOrOfficeAddress = 2,
        /// <summary>
        /// An address used for receiving mail, which may or may not be the same as the home or work address.
        /// </summary>
        MailingAddress = 3,
        /// <summary>
        /// The address associated with a person's payment method, typically used for billing and financial transactions.
        /// </summary>
        BillingAddress = 4,
        /// <summary>
        /// The address where goods or packages are to be delivered, often different from the billing address.
        /// </summary>
        ShippingAddress = 5,
        /// <summary>
        /// An address formatted for alternative home or postal delivery, including components such as street name, house number, city, state/province, postal code, and country.
        /// </summary>
        AlternativeHomeAddress = 6,
    }

    public enum GendersEnum
    {
        /// <summary>
        /// Refers to individuals who identify as male.
        /// </summary>
        Male = 0,

        /// <summary>
        /// Refers to individuals who identify as female.
        /// </summary>
        Female = 1
    }
}
