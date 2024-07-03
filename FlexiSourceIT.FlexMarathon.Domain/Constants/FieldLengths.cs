namespace FlexiSourceIT.FlexMarathon.Domain.Constants;

public static class FieldLengths
{
    public static class General
    {
        public const int Name = 80;
        public const int PhoneNumber = 20;
        public const int EmailAddress = 200;
        public const int URL = 2400;

        public const int ExtremelyShort = 5;
        public const int ExtraShort = 10;
        public const int Short = 20;
        public const int Medium = 50;
        public const int Long = 150;
        public const int ExtraLong = 300;
        public const int SuperLong = 1000;
        public const int HyperLong = 2400;
        public const int SummaryParagraph = 800;
    }

/*    public static class Activity {
        public required string Location { get; set; }
        public required DateTime DateTimeStarted { get; set; }
        public DateTime? DateTimeEnded { get; set; }
        public double? Distance { get; set; }
        public TimeSpan? Duration { get; set; }
        public double? AveragePace { get; set; }
    }*/
}