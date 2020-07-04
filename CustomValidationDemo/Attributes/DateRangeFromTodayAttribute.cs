namespace CustomValidationDemo.Attributes
{
	using System;
	using System.ComponentModel.DataAnnotations;

	[AttributeUsage(AttributeTargets.Property)]
	public class DateRangeFromTodayAttribute : ValidationAttribute
	{
		public DatePart RangeType { get; set; }
		public int? StepsBefore { get; set; }
		public int? StepsAfter { get; set; }
		public bool IsDateOnly { get; set; }

		public DateRangeFromTodayAttribute(DatePart rangeType, int steps, StepType stepType, bool isDateOnly)
		{
			RangeType = rangeType;
			IsDateOnly = isDateOnly;

			if (stepType == StepType.BeforeToday)
				StepsBefore = steps;

			if (stepType == StepType.AfterToday)
				StepsAfter = steps;
		}

		public DateRangeFromTodayAttribute(DatePart rangeType, int beforeToday, int afterToday, bool isDateOnly)
		{
			RangeType = rangeType;
			IsDateOnly = isDateOnly;
			StepsBefore = beforeToday;
			StepsAfter = afterToday;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value != null && !(value is DateTime))
				throw new InvalidOperationException($"{nameof(DateRangeFromTodayAttribute)} can only be used on DateTime properties.");

			if (!(value is DateTime date))
				return ValidationResult.Success;

			DateTime minDate = GetMinDate();
			var minIsValid = date >= minDate;

			DateTime maxDate = GetMaxDate();
			var maxIsValid = date <= maxDate;

			return minIsValid && maxIsValid
				? ValidationResult.Success
				: new ValidationResult(ErrorMessage);
		}

		public DateTime GetMinDate()
		{
			return StepsBefore is null
				? DateTime.MinValue
				: GetCalculatedDate(-StepsBefore.Value);
		}

		public DateTime GetMaxDate()
		{
			return StepsAfter is null
				? DateTime.MaxValue
				: GetCalculatedDate(StepsAfter.Value);
		}

		private DateTime GetCalculatedDate(int steps)
		{
			DateTime now = IsDateOnly ? DateTime.Today : DateTime.Now;
			return RangeType switch
			{
				DatePart.Ticks => now.AddTicks(steps),
				DatePart.Milliseconds => now.AddMilliseconds(steps),
				DatePart.Seconds => now.AddSeconds(steps),
				DatePart.Minutes => now.AddMinutes(steps),
				DatePart.Hours => now.AddHours(steps),
				DatePart.Days => now.AddDays(steps),
				DatePart.Months => now.AddMonths(steps),
				DatePart.Years => now.AddYears(steps),
				_ => throw new ArgumentOutOfRangeException()
			};
		}
	}

	public enum DatePart
	{
		Ticks,
		Milliseconds,
		Seconds,
		Minutes,
		Hours,
		Days,
		Months,
		Years,
	}

	public enum StepType
	{
		BeforeToday,
		AfterToday,
	}
}