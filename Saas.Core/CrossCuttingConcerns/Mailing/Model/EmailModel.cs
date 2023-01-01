using System.Collections.Generic;
using System.IO;

namespace Saas.Core.CrossCuttingConcerns.Mailing.Model
{
    internal class EmailModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public Priority Priority { get; set; }
        public IList<Address> ToAddresses { get; set; }
        public IList<Address> CcAddresses { get; set; }
        public IList<Address> BccAddresses { get; set; }
        public IList<Address> ReplyToAddresses { get; set; }
        public IList<Attachment> Attachments { get; set; }
        public Address FromAddress { get; set; }

        public EmailModel()
        {
            ToAddresses = new List<Address>();
            CcAddresses = new List<Address>();
            BccAddresses = new List<Address>();
            ReplyToAddresses = new List<Address>();
            Attachments = new List<Attachment>();
           
        }
    }
    public class Attachment
    {
        /// <summary>
        /// Gets or sets whether the attachment is intended to be used for inline images (changes the parameter name for providers such as MailGun)
        /// </summary>
        public bool IsInline { get; set; }
        public string Filename { get; set; }
        public Stream Data { get; set; }
        public string ContentType { get; set; }
        public string ContentId { get; set; }
    }
    public enum Priority
    {
        High = 1,
        Normal = 2,
        Low = 3
    }
    public class Address
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }

        public Address()
        {
        }

        public Address(string emailAddress,string name = null)
        {
            EmailAddress = emailAddress;
            Name = name;
        }

        public override string ToString()
        {
            return Name == null ? EmailAddress : $"{Name} <{EmailAddress}>";
        }

        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Address otherAddress = (Address)obj;
                return this.EmailAddress == otherAddress.EmailAddress && this.Name == otherAddress.Name;
            }
        }
    }
    

    
}
