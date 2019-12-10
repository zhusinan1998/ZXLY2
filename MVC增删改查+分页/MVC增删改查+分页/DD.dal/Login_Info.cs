namespace DD.dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Login_Info
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("用户名")]
        public string UserName { get; set; }
        [DisplayName("密码")]
        public int UserPwd { get; set; }
        [DisplayName("性别")]
        public int? Usersex { get; set; }
        [DisplayName("出生日期")]
        public DateTime? Usertime { get; set; }

        [StringLength(200)]
        [DisplayName("家庭住址")]
        public string Useraddress { get; set; }

        [StringLength(50)]
        [DisplayName("用户邮箱")]
        public string UserEmail { get; set; }
        [DisplayName("用户身份")]
        public int? Roleid { get; set; }
        [DisplayName("用户身份")]
        public string Rolename
        {
            get
            {
                if (Roleid == 1)
                {
                    return "村长";
                }
                else if (Roleid == 3)
                {
                    return "副村长";
                }
                else
                {
                    return "村民";
                }
            }
        } 
        }
        //public virtual Login_role Login_role { get; set; }
    }

