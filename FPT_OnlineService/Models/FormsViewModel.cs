using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace FPT_OnlineService.Models
{
    public class Form
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Form type: ")]
        public string Type { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string CurrentDesk { get; set; }

        public string Flow { get; set; }

        public string Status { get; set; }

        public string FormDetails { get; set; }

        [Display(Name = "Approved / Disapproved By: ")]
        public string ApprovalBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Approval date: ")]
        public string ApprovalDate { get; set; }

        [Display(Name="Student RollNo: ")]
        public string StudentRollNo { get; set; }
        
        [Display(Name = "Student Name: ")]
        public string StudentName { get; set; }

        [Display(Name = "Student Email: ")]
        public string StudentEmail { get; set; }

        [Display(Name = "Student Phone: ")]
        public string StudentPhone { get; set; }

        //Spring Fall Summer
        [ForeignKey("Semester")]
        [Display(Name = "Semester's: ")]
        public string SemesterName { get; set; }

        public virtual Semester Semester { get; set; }
        public virtual CourseRegForm CourseRegForm { get; set; }
        public virtual DropOutForm DropOutForm { get; set; }
        public virtual SuspendSemesterForm SuspendSemesterForm { get; set; }
        public virtual SuspendSubjectForm SuspendSubjectForm { get; set; }
        public virtual RequestForm RequestForm { get; set; }
    }

    public class DropOutForm
    {
        [Key]
        [ForeignKey("Form")]
        public int FormID { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Please input your correct StudentRollNo", MinimumLength = 6)]
        [Display(Name = "Student Class (Lớp): ")]
        public string Class { get; set; }

        [Required]
        [Display(Name = "Method of payment (Hỉnh thức nộp học phí): ")]
        public string MethodPayment { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of dropping out (Ngày thôi học): ")]
        public string DropOutDate { get; set; }

        [Required]
        [Display(Name = "Reason (Lý do): ")]
        [DataType(DataType.MultilineText)]
        public string Reason { get; set; }

        [Display(Name = "Student Library Status: ")]
        public string LibraryStatus { get; set; }

        [Display(Name = "Student Account Status: ")]
        public string AccountStatus { get; set; }

        [Display(Name = "Accademic Head Note: ")]
        public string AcademicHeadEndorse { get; set; }
        
        public virtual Form Form { get; set; }
    }

    public class SuspendSemesterForm
    {
        [Key]
        [ForeignKey("Form")]
        public int FormID { get; set; }
                
        [Display(Name = "Tuition Fee: ")]
        public bool TuitionFee { get; set; }

        //Staff Part
        [Display(Name = "Previous semester: ")]
        public string PreviousSemester { get; set; }

        [Display(Name = "Semester before the last: ")]
        public string TwoPrevSemester { get; set; }

        [Display(Name = "Reason (Lý do): ")]
        [DataType(DataType.MultilineText)]
        public string Reason { get; set; }

        public bool IsWeekBefore { get; set; }

        public virtual Form Form { get; set; }
    }

    public class SuspendSubjectForm
    {
        [Key]
        [ForeignKey("Form")]
        public int FormID { get; set; }
        
        [Required]
        [Display(Name = "Subject code: ")]
        public string SubjectCode { get; set; }

        [Required]
        [Display(Name = "Subject Name: ")]
        public string SubjectName { get; set; }


        [Display(Name = "Not All Subject: ")]
        public bool NotAllSubject { get; set; }

        [Display(Name = "Reason (Lý do): ")]
        [DataType(DataType.MultilineText)]
        public string Reason { get; set; }

        public bool IsWeekBefore { get; set; }

        public virtual Form Form { get; set; }

    }

    public class RequestForm
    {
        [Key]
        [ForeignKey("Form")]
        public int FormID { get; set; }

        [Required]
        [Display(Name = "Request Title: ")]
        public string RequestTitle { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Please input your correct details", MinimumLength = 6)]
        [Display(Name = "Student Class (Lớp): ")]
        public string Class { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "Field cannot be longer than 500 characters.")]
        [Display(Name = "I am requesting for/I would like to explain my problem as followed: ")]
        public string Description { get; set; }

        [Display(Name = "Processed By: ")]
        public string ProcessedBy { get; set; }

        [Display(Name = "Reasons for approval / disapproval: ")]
        public string ApprovalReason { get; set; }


        [Display(Name = "To department: ")]
        public string ToDepartment { get; set; }

        [Display(Name = "Received by: ")]
        public string ReceivedBy { get; set; }

        public virtual Form Form { get; set; }
    }

    public class CourseRegForm
    {
        [Key]
        [ForeignKey("Form")]
        public int FormID { get; set; }
        
        [Required]
        [Display(Name = "Subject Code(Mã môn): ")]
        public string SubjectCode { get; set; }

        [Required]
        [Display(Name = "Subject(Môn đăng ký): ")]
        public string Subject { get; set; }

        [Display(Name = "Courses available: ")]
        public string CoursesAvailable { get; set; }

        [Display(Name = "Tuition Fee: ")]
        public bool TuitionFee { get; set; }

        [Display(Name = "Is week before: ")]
        public bool IsWeekBefore { get; set; }

        public virtual Form Form { get; set; }
    }

    public class FormComment
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Form")]
        public int FormID { get; set; }

        [ForeignKey("IdentityRole")]
        public string RoleID { get; set; }

        public string Comment { get; set; }

        public virtual IdentityRole IdentityRole { get; set; }
        public virtual Form Form { get; set; }
    }

    public class AllFormModel
    {
        public Form Form { get; set; }

        public CourseRegForm CourseRegForm { get; set; }

        public SuspendSemesterForm SuspendSemesterForm { get; set; }

        public SuspendSubjectForm SuspendSubjectForm { get; set; }

        public DropOutForm DropOutForm { get; set; }

        public RequestForm RequestForm { get; set; }

        public Notification Notification { get; set; }
    }

    public class FormsReport
    {
        public IEnumerable<Semester> Semesters { get; set; }

        public IEnumerable<Form> Forms { get; set; }

        public IEnumerable<CourseRegForm> CourseRegForms { get; set; }

        public IEnumerable<SuspendSemesterForm> SuspendSemesterForms { get; set; }

        public IEnumerable<SuspendSubjectForm> SuspendSubjectForms { get; set; }

        public IEnumerable<DropOutForm> DropOutForms { get; set; }

        public IEnumerable<RequestForm> RequestForms { get; set; }

        public IEnumerable<Notification> Notifications { get; set; }
    }
}