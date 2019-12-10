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
        [DisplayName("�û���")]
        public string UserName { get; set; }
        [DisplayName("����")]
        public int UserPwd { get; set; }
        [DisplayName("�Ա�")]
        public int? Usersex { get; set; }
        [DisplayName("��������")]
        public DateTime? Usertime { get; set; }

        [StringLength(200)]
        [DisplayName("��ͥסַ")]
        public string Useraddress { get; set; }

        [StringLength(50)]
        [DisplayName("�û�����")]
        public string UserEmail { get; set; }
        [DisplayName("�û����")]
        public int? Roleid { get; set; }
        [DisplayName("�û����")]
        public string Rolename
        {
            get
            {
                if (Roleid == 1)
                {
                    return "�峤";
                }
                else if (Roleid == 3)
                {
                    return "���峤";
                }
                else
                {
                    return "����";
                }
            }
        } 
        }
        //public virtual Login_role Login_role { get; set; }
    }

