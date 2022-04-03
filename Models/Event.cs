using System;

namespace TelerivetExample.Models
{
    public partial class Event
	{
		/// <summary>
		/// Gets or sets the EventID value.
		/// </summary>
		public int EventID { get; set; }

		/// <summary>
		/// Gets or sets the Content value.
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Gets or sets the FromNumber value.
		/// </summary>
		public string FromNumber { get; set; }

		/// <summary>
		/// Gets or sets the PhoneID value.
		/// </summary>
		public string PhoneID { get; set; }

		/// <summary>
		/// Gets or sets the CreatedDate value.
		/// </summary>
		public DateTime CreatedDate { get; set; }

    }
}
