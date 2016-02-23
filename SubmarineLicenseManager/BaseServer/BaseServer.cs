using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;

namespace Submarine.Base
{
    public enum ServerOp
    {
        AUTHENTICATE, LOG_OUT, GET_OL_STATS, GET_SOFTWARES, GET_SOFTWARE_LICENSES, GET_SOFTWARE_LICENSES_OL, GET_SOFTWARE_VERSIONS, GET_SOFTWARE_VERSIONS_OL, GET_SOFTWARE_CUSTOMERS, GET_LICENSE_SERVER_USERS
    }

    public interface IQueryResult
    {
        int TotalCnt { get; set; }
        int From { get; set; }
        IList IList { get; set; }
    }

    [Serializable]
    public class QueryResult<T> : IQueryResult
    {
        public QueryResult(List<T> List, int TotalCnt, int From)
        {
            this.List = List;
            this.TotalCnt = TotalCnt;
            this.From = From;
        }
        public int TotalCnt { get; set; }
        public List<T> List { get; set; }
        public IList IList { get { return List; } set { List = (List<T>)value; } }
        public int From { get; set; }
    }

    [Serializable]
    public class OnlineStatsRow
    {
        public int NumAllSoftwares { get; set; }
        public int NumOnlineSoftwares { get; set; }
        public int NumAllLicenses { get; set; }
        public int NumOnlineLicenses { get; set; }
        public int NumAllCustomers { get; set; }
        public int NumOnlineCustomers { get; set; }
        public int NumAllUsers { get; set; }
        public int NumOnlineUsers { get; set; }
    }

    [Serializable]
    public class SoftwareRow
    {
        public SoftwareRow()
        {
        }
        public SoftwareRow(int SoftwareId)
        {
            this.SoftwareId = SoftwareId;
        }

        public int SoftwareId { get; set; }

        [StringLength(256)]
        public string Title { get; set; }

        public int CurrentVersionId { get; set; }

        [StringLength(256)]
        public string CurrentVersionTitle { get; set; }
        public DateTime CurrentVersionAvailableFrom { get; set; }
        public DateTime CurrentVersionAvailableUntil { get; set; }

        public int UpdateLogId { get; set; }

        public DateTime CreateTime { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdatedByName { get; set; }

        public override bool Equals(object o)
        {
            return ((SoftwareRow)o).SoftwareId == SoftwareId;
        }
        public override int GetHashCode()
        {
            return SoftwareId;
        }
    }

    [Serializable]
    public class LicenseRow
    {
        public LicenseRow()
        {
        }
        public LicenseRow(int LincenseId)
        {
            this.LicenseId = LincenseId;
        }
        public int LicenseId { get; set; }

        [StringLength(256)]
        public string OriginalId { get; set; }
        [StringLength(1024)]
        public string Key { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }

        public DateTime LastLoginTime { get; set; }
        public DateTime LastLogoutTime { get; set; }

        public bool Online { get; set; }

        [ForeignKey("Software")]
        public int SoftwareId { get; set; }

        [StringLength(256)]
        public string SoftwareTitle { get; set; }

        public int CustomerId { get; set; }

        [StringLength(256)]
        public string CustomerName { get; set; }
        [StringLength(256)]
        public string CustomerEmail { get; set; }

        public int CurrentVersionId { get; set; }

        [StringLength(256)]
        public string CurrentVersionTitle { get; set; }
        public DateTime CurrentVersionAvailableFrom { get; set; }
        public DateTime CurrentVersionAvailableUntil { get; set; }

        public int UpdateLogId { get; set; }

        public DateTime CreateTime { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdatedByName { get; set; }

        public override bool Equals(object o)
        {
            return ((LicenseRow)o).LicenseId == LicenseId;
        }
        public override int GetHashCode()
        {
            return LicenseId;
        }
    }

    [Serializable]
    public class VersionRow
    {
        public VersionRow()
        {
        }
        public VersionRow(int VersionId)
        {
            this.VersionId = VersionId;
        }
        public int VersionId { get; set; }

        [StringLength(256)]
        public string Title { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableUntil { get; set; }

        public int SoftwareId { get; set; }
        [StringLength(256)]
        public string SoftwareTitle { get; set; }

        public bool Current { get; set; }

        public int UpdateLogId { get; set; }

        public DateTime CreateTime { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdatedByName { get; set; }

        public int NumAllLicenses { get; set; }
        public int NumOnlineLicenses { get; set; }
        public int NumAllCustomers { get; set; }
        public int NumOnlineCustomers { get; set; }

        public string NumLicensesOA
        {
            get
            {
                return NumOnlineLicenses + "/" + NumAllLicenses;
            }
        }
        public string NumCustomersOA
        {
            get
            {
                return NumOnlineCustomers + "/" + NumAllCustomers;
            }
        }

        public override bool Equals(object o)
        {
            return ((VersionRow)o).VersionId == VersionId;
        }
        public override int GetHashCode()
        {
            return VersionId;
        }
    }

    [Serializable]
    public class PersonRow
    {
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(256)]
        public string Phone { get; set; }

        public DateTime LastLoginTime { get; set; }
        public DateTime LastLogoutTime { get; set; }
        public bool Online { get; set; }

    }

    [Serializable]
    public class CustomerRow : PersonRow
    {
        public CustomerRow()
        {
        }
        public CustomerRow(int LincenseId)
        {
            this.LicenseId = LincenseId;
        }
        public int CustomerId { get; set; }

        public int LicenseId { get; set; }

        [StringLength(256)]
        public string LicenseOriginalId { get; set; }
        public DateTime LicenseValidFrom { get; set; }
        public DateTime LicenseValidUntil { get; set; }

        public int UpdateLogId { get; set; }

        public DateTime CreateTime { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdatedByName { get; set; }

        public override bool Equals(object o)
        {
            return ((CustomerRow)o).LicenseId == LicenseId;
        }
        public override int GetHashCode()
        {
            return LicenseId;
        }
    }

    [Serializable]
    public class UserRow : PersonRow
    {
        public UserRow()
        {
        }
        public UserRow(int UserId)
        {
            this.UserId = UserId;
        }
        public int UserId { get; set; }

        [StringLength(32)]
        public string LoginId { get; set; }
        [StringLength(1024)]
        public string EncryptedPassword { get; set; }
        public bool Admin { get; set; }

        public override bool Equals(object o)
        {
            return ((UserRow)o).UserId == UserId;
        }
        public override int GetHashCode()
        {
            return UserId;
        }
    }
}
