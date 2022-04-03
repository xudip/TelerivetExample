using System;

namespace TelerivetExample.Models
{
    public partial class UserProject
	{
		/// <summary>
		/// Gets or sets the UserProjectID value.
		/// </summary>
		public int UserProjectID { get; set; }

		/// <summary>
		/// Gets or sets the UserID value.
		/// </summary>
		public int UserID { get; set; }

		/// <summary>
		/// Gets or sets the ProjectCode value.
		/// </summary>
		public string ProjectCode { get; set; }

		/// <summary>
		/// Gets or sets the Amount value.
		/// </summary>
		public decimal Amount { get; set; }

		/// <summary>
		/// Gets or sets the Pin value.
		/// </summary>
		public string Pin { get; set; }

		/// <summary>
		/// Gets or sets the Status value.
		/// </summary>
		public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the RequestID value.
        /// </summary>
        public int RequestID { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate value.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the FromNumber value.
        /// </summary>
        public string FromNumber { get; set; }

        public string UserName { get; set; }

        // Foreign Key columns

        /// <summary>
        /// Gets or sets the User value.
        /// </summary>
        public User User { get; set; }


	}
}
