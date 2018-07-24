using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDBEntity4.Models
{
    public partial class MetaData
    {
       
    }

    [MetadataType(typeof(EmployeeMetaData))]
    public partial class Employee
    {
        
    }

    public class EmployeeMetaData
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        public int DepartmentId { get; set; }       
    }

    public partial class tbl_department_list
    {
        
    }

    [MetadataType(typeof(EmployeeDetailMetaData))]
    public partial class EmployeeDetail
    {
    }
    public partial class EmployeeDetailMetaData
    {
        //[HiddenInput(DisplayValue =false)]
        public int Id { get; set; }

        [Display(Name = "Fulll Name")]
        public string FullName { get; set; }

        [DisplayFormat(NullDisplayText ="Gender not Specified")]
        public string Gender { get; set; }

        //public Nullable<int> Age { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? HireDate { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.Currency)]
        public Nullable<int> Salary { get; set; }

        [DataType(DataType.Url)]
        [UIHint("OpenInNewTab")]
        public string PersonalWebsite { get; set; }
    }
}